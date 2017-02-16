using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;
using ExecutionContext = Slalom.Stacks.Runtime.ExecutionContext;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Supervises the execution and completion of commands.  Returns a result containing the returned value if the command is successful; 
    /// otherwise, returns information about why the execution was not successful.
    /// </summary>
    /// <seealso cref="ICommandCoordinator" />
    public class CommandCoordinator : ICommandCoordinator
    {
        private readonly Lazy<IEnumerable<IAuditStore>> _audits;
        private readonly ReaderWriterLockSlim _validatorsLock = new ReaderWriterLockSlim();
        private readonly IComponentContext _context;
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _handlers = new ConcurrentDictionary<Type, IEnumerable<object>>();
        private readonly Lazy<ILogger> _logger;
        private readonly Lazy<IEnumerable<IRequestStore>> _requests;
        private readonly Lazy<IEventPublisher> _publisher;
        private readonly Dictionary<Type, ICommandValidator> _validators = new Dictionary<Type, ICommandValidator>();
        private readonly Lazy<IExecutionContextResolver> _contextResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCoordinator"/> class.
        /// </summary>
        /// <param name="context">The configured <see cref="IComponentContext"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public CommandCoordinator(IComponentContext context)
        {
            Argument.NotNull(context, nameof(context));

            _context = context;
            _logger = new Lazy<ILogger>(() => _context.Resolve<ILogger>());
            _publisher = new Lazy<IEventPublisher>(() => _context.Resolve<IEventPublisher>());
            _audits = new Lazy<IEnumerable<IAuditStore>>(() => _context.ResolveAll<IAuditStore>());
            _requests = new Lazy<IEnumerable<IRequestStore>>(() => _context.ResolveAll<IRequestStore>());
            _contextResolver = new Lazy<IExecutionContextResolver>(() => _context.Resolve<IExecutionContextResolver>());
        }

        /// <summary>
        /// Handles the command, progressing it through the stages of the pipeline.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        public Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return this.SendAsync(null, command, timeout);
        }

        /// <summary>
        /// Handles the command, progressing it through the stages of the pipeline.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="path"></param>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        public async Task<CommandResult> SendAsync(string path, ICommand command, TimeSpan? timeout = null)
        {
            Argument.NotNull(command, nameof(command));

            _logger.Value.Verbose("Starting execution for " + command.Type + " at \"{Path}\". {@Command}", path, command);

            // get the context
            var context = _contextResolver.Value.Resolve();

            context.SetPath(path);

            // create the result
            var result = new CommandResult(context);

            try
            {
                // validate the command
                await this.ValidateCommand(command, result, context);

                if (!result.ValidationErrors.Any())
                {
                    // execute the handler
                    await this.ExecuteHandler(command, path, result, context);

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
            var tasks = _requests.Value.Select(e => e.AppendAsync(new RequestEntry(command, result, context))).ToList();

            if (!result.IsSuccessful)
            {
                if (result.RaisedException != null)
                {
                    _logger.Value.Error(result.RaisedException, "An unhandled exception was raised while executing " + command.CommandName + ". {@Command} {@Context}", command, context);
                }
                else if (result.ValidationErrors?.Any() ?? false)
                {
                    _logger.Value.Verbose("Execution completed with validation errors while executing " + command.CommandName + ". {@Command} {@ValidationErrors} {@Context}", command, result.ValidationErrors, context);
                }
                else
                {
                    _logger.Value.Error("Execution completed unsuccessfully while executing " + command.CommandName + ". {@Command} {@Result} {@Context}", command, result, context);
                }
            }
            else
            {
                _logger.Value.Verbose("Successfully completed " + command.CommandName + ". {@Command} {@Result} {@Context}", command, result, context);
            }
            foreach (var instance in context.RaisedEvents)
            {
                tasks.AddRange(_audits.Value.Select(e => e.AppendAsync(new AuditEntry(instance, context))));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// The command execution stage of the pipeline.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="path">The path.</param>
        /// <param name="result">The result.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        protected virtual async Task ExecuteHandler(ICommand command, string path, CommandResult result, ExecutionContext context)
        {
            var handlers = _context.ResolveAll(typeof(IHandle<>).MakeGenericType(command.GetType())).ToList();
            IHandle handler;
            if (String.IsNullOrWhiteSpace(path))
            {
                handler = (IHandle)handlers.FirstOrDefault(e => !e.GetType().GetTypeInfo().GetCustomAttributes<PathAttribute>().Any()) ??
                          (IHandle)handlers.FirstOrDefault();
            }
            else
            {
                handler = (IHandle)handlers.FirstOrDefault(e => e.GetType().GetTypeInfo().GetCustomAttributes<PathAttribute>().Any(x => x.Path == path));
                if (handler == null && handlers.Count() == 1)
                {
                    handler = (IHandle)handlers.FirstOrDefault();
                }
            }
            if (handler == null)
            {
                throw new InvalidOperationException($"The actor could be found for {command.CommandName} at \"{path}\".");
            }

            handler.SetContext(context);

            var response = await handler.HandleAsync(command);

            result.AddResponse(response);
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
                try
                {
                    await _publisher.Value.PublishAsync(item, context);
                }
                catch (Exception exception)
                {
                    _logger.Value.Error(exception, "An unhandled exception was raised while handling " + item.EventName + ": {@Event} {@Context}", item, context);
                }
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
            ICommandValidator target;
            if (!_validators.TryGetValue(command.GetType(), out target))
            {
                _validatorsLock.EnterWriteLock();
                try
                {
                    if (!_validators.TryGetValue(command.GetType(), out target))
                    {
                        target = (ICommandValidator)_context.Resolve(typeof(CommandValidator<>).MakeGenericType(command.GetType()));
                        _validators.Add(command.GetType(), target);
                    }
                }
                finally
                {
                    _validatorsLock.ExitWriteLock();
                }
            }

            var errors = await target.Validate(command, context);

            result.AddValidationErrors(errors);
        }

        public Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null)
        {
            var actors = _context.Resolve<IDiscoverTypes>().Find(typeof(IHandle<>)).Where(e => e.GetTypeInfo().GetCustomAttributes<PathAttribute>().Any(x => x.Path == path)).ToList();
            if (actors.Count() > 1)
            {
                throw new InvalidOperationException($"More than one actor has been configured for the path {path}.");
            }
            if (actors.Count == 0)
            {
                throw new InvalidOperationException($"No actor has been configured for the path {path}.");
            }

            var commandType = actors.First().GetTypeInfo().BaseType.GetGenericArguments()[0];

            var instance = (ICommand)JsonConvert.DeserializeObject(!string.IsNullOrWhiteSpace(command) ? command : "{}", commandType);

            return this.SendAsync(path, instance, timeout);
        }
    }
}