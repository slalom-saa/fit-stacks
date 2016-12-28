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
using Slalom.Stacks.Messaging.Logging;
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
        private readonly Lazy<ILogger> _logger;
        private readonly Lazy<IEventPublisher> _publisher;
        private readonly Lazy<IEnumerable<IAuditStore>> _audits;
        private readonly Lazy<IEnumerable<ILogStore>> _logs;
        private readonly ConcurrentDictionary<Type, ICommandValidator> _validators = new ConcurrentDictionary<Type, ICommandValidator>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UseCaseCoordinator"/> class.
        /// </summary>
        /// <param name="context">The configured <see cref="IComponentContext"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public UseCaseCoordinator(IComponentContext context)
        {
            Argument.NotNull(context, nameof(context));

            _context = context;
            _logger = new Lazy<ILogger>(() => _context.Resolve<ILogger>());
            _publisher = new Lazy<IEventPublisher>(() => _context.Resolve<IEventPublisher>());
            _audits = new Lazy<IEnumerable<IAuditStore>>(() => _context.ResolveAll<IAuditStore>());
            _logs = new Lazy<IEnumerable<ILogStore>>(() => _context.ResolveAll<ILogStore>());
        }

        /// <summary>
        /// Handles the command, progressing it through the stages of the pipeline.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        public async Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            Argument.NotNull(command, nameof(command));

            _logger.Value.Verbose("Starting execution for " + command.CommandName + ". {@Command}", command);

            // Create the result
            var context = _context.Resolve<IExecutionContextResolver>().Resolve();
            var result = new CommandResult(context);

            try
            {
                // validate the command
                await this.ValidateCommand(command, result, context);

                if (!result.ValidationErrors.Any())
                {
                    // execute the handler
                    await this.ExecuteUseCase(command, result, context);

                    // add the response to the context if it was an event
                    var value = result.Response as IEvent;
                    if (value != null)
                    {
                        context.AddRaisedEvent(value);
                    }
                }

                // publish all events
                await this.PublishEvents(context);
            }
            catch (Exception exception)
            {
                // handle any exceptions
                this.HandleException(command, context, result, exception);
            }

            // finalize the result and mark it as complete
            result.Complete();

            // audit the results
            await this.Log(command, result, context);

            return result;
        }

        /// <summary>
        /// The audit stage of the pipeline.
        /// </summary>
        /// <param name="command">The executed command.</param>
        /// <param name="result">The result.</param>
        /// <param name="context">The execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual Task Log(ICommand command, CommandResult result, ExecutionContext context)
        {
            var tasks = _logs.Value.Select(e => e.AppendAsync(new LogEntry(command, result, context))).ToList();

            // Add additional audits and diagnostic messages if the result was unsuccessful.
            if (!result.IsSuccessful)
            {
                if (result.RaisedException != null)
                {
                    _logger.Value.Verbose(result.RaisedException, "An unhandled exception was raised while executing " + command.CommandName + ". {@Command} {@Context}", command, context);
                }
                else if (result.ValidationErrors?.Any() ?? false)
                {
                    _logger.Value.Verbose("Execution completed with validation errors while executing " + command.CommandName + ". {@Command} {@ValidationErrors} {@Context}", command, result.ValidationErrors, context);
                }
                else
                {
                    _logger.Value.Verbose("Execution completed unsuccessfully while executing " + command.CommandName + ". {@Command} {@Result} {@Context}", command, result, context);
                }
            }
            else
            {
                if (result.Response is IEvent)
                {
                    tasks.AddRange(_audits.Value.Select(e => e.AppendAsync(new AuditEntry(result.Response as IEvent, context))));
                }
                _logger.Value.Verbose("Successfully completed " + command.CommandName + ". {@Command} {@Result} {@Context}", command, result, context);
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// The command execution stage of the pipeline.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="result">The result.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        protected virtual async Task ExecuteUseCase(ICommand command, CommandResult result, ExecutionContext context)
        {
            var handler = (dynamic)_handlers.GetOrAdd(command.GetType(), key =>
            {
                var actors = _context.Resolve<IDiscoverTypes>().Find(typeof(UseCaseActor<,>));
                var type = actors.FirstOrDefault(e => e.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType && e.GetTypeInfo().BaseType.GetGenericArguments().FirstOrDefault() == command.GetType());

                return _context.Resolve(type);
            });

            if (handler == null)
            {
                throw new InvalidOperationException($"An actor could not be found for the specified command: {command.CommandName}.");
            }

            IEnumerable<ValidationError> errors = await handler.ValidateAsync((dynamic)command, context);

            result.AddValidationErrors(errors);
            if (!result.ValidationErrors.Any())
            {
                object response = await handler.ExecuteAsync((dynamic)command, context);

                result.AddResponse(response);
            }
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
        protected virtual void HandleException(ICommand command, ExecutionContext context, CommandResult result, Exception exception)
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
                await _publisher.Value.PublishAsync(item, context);
            }
        }

        /// <summary>
        /// The validation stage of the pipeline.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="result">The result.</param>
        /// <param name="context">The context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        protected async Task ValidateCommand(ICommand command, CommandResult result, ExecutionContext context)
        {
            var target = _validators.GetOrAdd(command.GetType(), (ICommandValidator)_context.Resolve(typeof(CommandValidator<>).MakeGenericType(command.GetType())));

            var errors = await target.Validate(command, context);

            result.AddValidationErrors(errors);
        }
    }
}