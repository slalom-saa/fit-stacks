using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Communication.Logging;
using Slalom.Stacks.Communication.Validation;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Supervises the execution and completion of commands.  Returns a result containing the returned value if the command is successful; 
    /// otherwise, returns information about why the execution was not successful.
    /// </summary>
    /// <seealso cref="ICommandCoordinator" />
    public class CommandCoordinator : ICommandCoordinator
    {
        private readonly IComponentContext _componentContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICommandValidator _validator;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCoordinator"/> class.
        /// </summary>
        /// <param name="componentContext">The configured <see cref="IComponentContext"/> instance.</param>
        /// <param name="eventPublisher">The configured <see cref="IEventPublisher"/> instance.</param>
        /// <param name="validator">The configured <see cref="ICommandValidator"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="componentContext"/> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="eventPublisher"/> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="validator"/> argument is null.</exception>
        public CommandCoordinator(IComponentContext componentContext, IEventPublisher eventPublisher, ICommandValidator validator)
        {
            Argument.NotNull(() => componentContext);
            Argument.NotNull(() => eventPublisher);
            Argument.NotNull(() => validator);

            _componentContext = componentContext;
            _eventPublisher = eventPublisher;
            _validator = validator;
            _logger = _componentContext.Resolve<ILogger>();
        }

        /// <summary>
        /// Handles the command, progressing it through the stages of the pipeline.
        /// </summary>
        /// <typeparam name="TResult">The return type of the command.</typeparam>
        /// <param name="command">The command to handle.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        public async Task<CommandResult<TResult>> Handle<TResult>(Command<TResult> command, ExecutionContext context)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);

            _logger.Verbose("Starting execution for " + command.CommandName + ". {@Command}", command);

            // Create the result
            var result = new CommandResult<TResult>(context);

            try
            {
                // Validate the command
                result.AddValidationErrors(await _validator.Validate(command, context));

                if (!result.ValidationErrors?.Any() ?? false)
                {
                    // Execute the handler
                    result.Value = await this.ExecuteHandler<TResult>(command, context);

                    var value = result.Value as IEvent;
                    if (value != null)
                    {
                        context.AddRaisedEvent(value);
                    }
                }

                await this.PublishEvents(context);
            }
            catch (Exception exception)
            {
                // Handle any exceptions
                await this.HandleException(command, context, result, exception);
            }

            result.Complete();

            // Audit the results
            await this.Audit(command, result, context);

            return result;
        }

        /// <summary>
        /// The audit stage of the pipeline.
        /// </summary>
        /// <typeparam name="TResult">The type of response.</typeparam>
        /// <param name="command">The executed command.</param>
        /// <param name="result">The result.</param>
        /// <param name="context">The execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected async virtual Task Audit<TResult>(Command<TResult> command, CommandResult<TResult> result, ExecutionContext context)
        {
            // Log the command
            var logs = _componentContext.ResolveAll<ILogStore>().ToList();
            foreach (var item in logs)
            {
                await item.AppendAsync(command, result, context);
            }

            // Add an audit if state was changed
            if (result.Value is IEvent)
            {
                var stores = _componentContext.ResolveAll<IAuditStore>().ToList();
                foreach (var item in stores)
                {
                    await item.AppendAsync((IEvent)result.Value, context);
                }
            }

            // Add additional audits and diagnostic messages if the result was unsuccessful.
            if (!result.IsSuccessful)
            {
                var stores = _componentContext.ResolveAll<IAuditStore>().ToList();
                stores.ForEach(async e => await e.AppendAsync(new CommandExecutionFailedEvent(command, result), context));
                if (result.RaisedException != null)
                {
                    _logger.Verbose(result.RaisedException, "An unhandled exception was raised while executing " + command.CommandName + ". {@Command} {@Context}", command, context);
                }
                else if (result.ValidationErrors?.Any() ?? false)
                {
                    _logger.Verbose("Execution completed with validation errors while executing " + command.CommandName + ". {@Command} {@ValidationErrors} {@Context}", command, result.ValidationErrors, context);
                }
                else
                {
                    _logger.Verbose("Execution completed unsuccessfully while executing " + command.CommandName + ". {@Command} {@Result} {@Context}", command, result, context);
                }
            }
            else
            {
                _logger.Verbose("Successfully completed " + command.CommandName + ". {@Command} {@Result} {@Context}", command, result, context);
            }
        }

        /// <summary>
        /// The command execution stage of the pipeline.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command"/> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        protected virtual async Task<TResult> ExecuteHandler<TResult>(ICommand command, ExecutionContext context)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);

            var handler = (dynamic)_componentContext.Resolve(typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult)));

            if (handler == null)
            {
                throw new InvalidOperationException($"A handler could not be found for the specified command: {command.CommandName}.");
            }

            handler.Context = context;

            return await handler.Handle((dynamic)command);
        }

        /// <summary>
        /// The exception handling stage of the pipeline.  This class specifically unwraps exceptions that are raised when multi-threading
        /// or invoking methods dynamically - in order to provide better messaging.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="command">The command that was executed.</param>
        /// <param name="context">The current execution context.</param>
        /// <param name="result">The result to build.</param>
        /// <param name="exception">The exception that was raised.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command"/> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        protected virtual Task HandleException<TResult>(Command<TResult> command, ExecutionContext context, CommandResult<TResult> result, Exception exception)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);
            Argument.NotNull(() => result);
            Argument.NotNull(() => exception);

            var validationException = exception as ValidationException;
            if (validationException != null)
            {
                result.AddValidationErrors(validationException.ValidationMessages);
            }
            else if (exception is AggregateException)
            {
                var innerException = exception.InnerException as ValidationException;
                if (innerException != null)
                {
                    result.AddValidationErrors(innerException.ValidationMessages);
                }
                else if (exception.InnerException is TargetInvocationException)
                {
                    result.AddException(((TargetInvocationException)exception.InnerException).InnerException);
                }
                else
                {
                    result.AddException(exception.InnerException);
                }
            }
            else if (exception is TargetInvocationException)
            {
                result.AddException(exception.InnerException);
            }
            else
            {
                result.AddException(exception);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// The event publishing stage of the pipeline.
        /// </summary>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        protected virtual Task PublishEvents(ExecutionContext context)
        {
            Argument.NotNull(() => context);

            return Task.WhenAll(context.RaisedEvents.Select(e => _eventPublisher.Publish(e, context)));
        }

        /// <summary>
        /// The validation stage of the pipeline.
        /// </summary>
        /// <typeparam name="TResult">The return type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="context">The context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        protected Task<IEnumerable<ValidationError>> ValidateCommand<TResult>(Command<TResult> command, ExecutionContext context)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);

            return _validator.Validate(command, context);
        }
    }
}