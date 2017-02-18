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
using Slalom.Stacks.Domain;
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
        private readonly Lazy<IEnumerable<IEventStore>> _audits;
        private readonly ReaderWriterLockSlim _validatorsLock = new ReaderWriterLockSlim();
        private readonly IComponentContext _context;
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _handlers = new ConcurrentDictionary<Type, IEnumerable<object>>();
        private readonly Lazy<ILogger> _logger;
        private readonly Lazy<IEnumerable<IRequestStore>> _requests;
        private readonly Lazy<IEventStream> _publisher;
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
            _publisher = new Lazy<IEventStream>(() => _context.Resolve<IEventStream>());
            _audits = new Lazy<IEnumerable<IEventStore>>(() => _context.ResolveAll<IEventStore>());
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
        public Task<CommandResult> SendAsync(IMessage command, TimeSpan? timeout = null)
        {
            return this.SendAsync(null, command, timeout);
        }

        public Task<CommandResult> SendAsync(string path, IMessage command, TimeSpan? timeout = null)
        {
            return this.SendAsync(path, command, null, null);
        }

        /// <summary>
        /// Handles the command, progressing it through the stages of the pipeline.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="command">The command to handle.</param>
        /// <param name="context">The context.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        public async Task<CommandResult> SendAsync(string path, IMessage command, ExecutionContext context = null, TimeSpan? timeout = null)
        {
            Argument.NotNull(command, nameof(command));

            _logger.Value.Verbose("Starting execution for " + command.GetType().Name + " at \"{Path}\". {@Command}", path, command);

            // get the context
            context = context ?? _contextResolver.Value.Resolve();
            context.Path = context.Path ?? path;
            context.Parent = context.Parent ?? command.Id;
            var events = context.RaisedEvents;

            // create the result
            var result = new CommandResult(context);

            try
            {
                // validate the command
                if (command is IMessage)
                {
                    await this.ValidateCommand((IMessage)command, result, context);
                }

                if (!result.ValidationErrors.Any())
                {
                    // execute the handler
                    await this.ExecuteHandler(command, path, result, context);
                }

                // publish all events
                this.PublishEvents(context.RaisedEvents.Except(events), context);
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
        protected virtual Task Log(IMessage command, CommandResult result, ExecutionContext context)
        {
            var tasks = _requests.Value.Select(e => e.AppendAsync(new RequestEntry(command, result, context))).ToList();

            var name = command.GetType().Name;

            if (!result.IsSuccessful)
            {
                if (result.RaisedException != null)
                {
                    _logger.Value.Error(result.RaisedException, "An unhandled exception was raised while executing " + command + ". {@Command} {@Context}", command, context);
                }
                else if (result.ValidationErrors?.Any() ?? false)
                {
                    _logger.Value.Verbose("Execution completed with validation errors while executing " + command + ". {@Command} {@ValidationErrors} {@Context}", command, result.ValidationErrors, context);
                }
                else
                {
                    _logger.Value.Error("Execution completed unsuccessfully while executing " + command + ". {@Command} {@Result} {@Context}", command, result, context);
                }
            }
            else
            {
                _logger.Value.Verbose("Successfully completed " + command + ". {@Command} {@Result} {@Context}", command, result, context);
            }
            foreach (var instance in context.RaisedEvents)
            {
                tasks.AddRange(_audits.Value.Select(e => e.AppendAsync(new EventEntry(instance, context))));
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
        protected virtual async Task ExecuteHandler(IMessage command, string path, CommandResult result, ExecutionContext context)
        {
            var handlers = _context.ResolveAll(typeof(IHandle<>).MakeGenericType(command.GetType())).ToList();
            if (String.IsNullOrWhiteSpace(path))
            {
                handlers = handlers.Where(e => !e.GetType().GetTypeInfo().GetCustomAttributes<PathAttribute>().Any()).ToList();
            }
            else
            {
                handlers = handlers.Where(e => e.GetType().GetTypeInfo().GetCustomAttributes<PathAttribute>().Any(x => x.Path == path)).ToList();
            }
            if (!handlers.Any())
            {
                throw new InvalidOperationException($"The actor could be found for {command.GetType().Name} at \"{path}\".");
            }

            foreach (IHandle instance in handlers)
            {
                instance.SetContext(context);

                var response = await instance.HandleAsync(command);

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
        protected virtual void HandleException(IMessage command, ExecutionContext context, CommandResult result, Exception exception)
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
        /// <param name="events">The current execution context.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="events" /> argument is null.</exception>
        protected virtual void PublishEvents(IEnumerable<Event> events, ExecutionContext context)
        {
            Argument.NotNull(events, nameof(events));

            foreach (var item in events)
            {
                try
                {
                    this.SendAsync(null, item, context, null).Wait();
                }
                catch (Exception exception)
                {
                    _logger.Value.Error(exception, "An unhandled exception was raised while publishing " + item.EventName + ": {@Event} {@Context}", item, events);
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
        protected async Task ValidateCommand(IMessage command, CommandResult result, ExecutionContext context)
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

        /// <summary>
        /// Sends the specified command with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null)
        {
            var actors = _context.Resolve<IDiscoverTypes>().Find(typeof(IHandle<>)).Where(e => e.GetTypeInfo().GetCustomAttributes<PathAttribute>().Any(x => x.Path == path)).ToList();
            if (actors.Count > 1)
            {
                throw new InvalidOperationException($"More than one actor has been configured for the path {path}.");
            }
            if (actors.Count == 0)
            {
                throw new InvalidOperationException($"No actor has been configured for the path {path}.");
            }

            var commandType = actors.First().GetTypeInfo().BaseType.GetGenericArguments()[0];

            var instance = (IMessage)JsonConvert.DeserializeObject(!string.IsNullOrWhiteSpace(command) ? command : "{}", commandType);

            return this.SendAsync(path, instance, timeout);
        }
    }
}