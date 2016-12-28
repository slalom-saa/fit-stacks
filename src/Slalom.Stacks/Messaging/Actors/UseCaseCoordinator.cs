using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Supervises the execution and completion of commands.  Returns a result containing the returned value if the command is successful; 
    /// otherwise, returns information about why the execution was not successful.
    /// </summary>
    /// <seealso cref="IUseCaseCoordinator" />
    public class UseCaseCoordinator : IUseCaseCoordinator
    {
        private readonly IComponentContext _context;
        private readonly ConcurrentDictionary<Type, object> _handlers = new ConcurrentDictionary<Type, object>();
        private readonly ConcurrentDictionary<Type, ICommandValidator> _validators = new ConcurrentDictionary<Type, ICommandValidator>();
        private readonly ILogger _logger;
        private readonly IEventPublisher _publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UseCaseCoordinator"/> class.
        /// </summary>
        /// <param name="context">The configured <see cref="IComponentContext"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public UseCaseCoordinator(IComponentContext context)
        {
            Argument.NotNull(context, nameof(context));

            _context = context;
            _logger = _context.Resolve<ILogger>();
            _publisher = _context.Resolve<IEventPublisher>();
        }

        /// <summary>
        /// Handles the command, progressing it through the stages of the pipeline.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        public async Task<CommandExecuted> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            Argument.NotNull(command, nameof(command));

            _logger.Verbose("Starting execution for " + command.CommandName + ". {@Command}", command);

            // Create the result
            var context = _context.Resolve<IExecutionContextResolver>().Resolve();
            var result = new CommandExecuted(context);

            try
            {
                // Validate the command
                result.AddValidationErrors(await this.ValidateCommand(command, context));

                if (!result.ValidationErrors.Any())
                {
                    // Execute the handler
                    result.AddResponse(await this.ExecuteHandler(command, context));

                    var value = result.Response as IEvent;
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
                this.HandleException(command, context, result, exception);
            }

            result.Complete();

            // Audit the results
            this.Audit(command, result, context);

            return result;
        }

        /// <summary>
        /// The audit stage of the pipeline.
        /// </summary>
        /// <param name="command">The executed command.</param>
        /// <param name="result">The result.</param>
        /// <param name="context">The execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual void Audit(ICommand command, CommandExecuted result, ExecutionContext context)
        {
            // Add additional audits and diagnostic messages if the result was unsuccessful.
            if (!result.IsSuccessful)
            {
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
        /// <param name="command">The command to execute.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command"/> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        protected virtual async Task<object> ExecuteHandler(ICommand command, ExecutionContext context)
        {
            var handler = (dynamic)_handlers.GetOrAdd(command.GetType(), key =>
            {
                var actors = _context.Resolve<IDiscoverTypes>().Find(typeof(UseCaseActor<,>));
                var type = actors.FirstOrDefault(e => e.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType && e.GetTypeInfo().BaseType.GetGenericArguments().FirstOrDefault() == command.GetType());

                return _context.Resolve(type);
            });

            if (handler == null)
            {
                throw new InvalidOperationException($"A handler could not be found for the specified command: {command.CommandName}.");
            }

            return await handler.ExecuteAsync((dynamic)command, context);
        }

        /// <summary>
        /// The exception handling stage of the pipeline.  This class specifically unwraps exceptions that are raised when multi-threading
        /// or invoking methods dynamically - in order to provide better messaging.
        /// </summary>
        /// <param name="command">The command that was executed.</param>
        /// <param name="context">The current execution context.</param>
        /// <param name="result">The result to build.</param>
        /// <param name="exception">The exception that was raised.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command"/> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        protected virtual void HandleException(ICommand command, ExecutionContext context, CommandExecuted result, Exception exception)
        {
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
        }

        /// <summary>
        /// The event publishing stage of the pipeline.
        /// </summary>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        protected virtual async Task PublishEvents(ExecutionContext context)
        {
            foreach (var item in context.RaisedEvents)
            {
                await _publisher.PublishAsync(item, context);
            }
        }

        /// <summary>
        /// The validation stage of the pipeline.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="context">The context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        protected async Task<IEnumerable<ValidationError>> ValidateCommand(ICommand command, ExecutionContext context)
        {
            var target = _validators.GetOrAdd(command.GetType(), (ICommandValidator)_context.Resolve(typeof(CommandValidator<>).MakeGenericType(command.GetType())));

            var result = await target.Validate(command, context);

            return result;
        }
    }
}