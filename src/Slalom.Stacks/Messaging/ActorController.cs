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
    public class ActorController
    {
        private readonly Lazy<IEnumerable<IEventStore>> _events;
        private readonly ReaderWriterLockSlim _validatorsLock = new ReaderWriterLockSlim();
        private readonly IComponentContext _context;
        private readonly Lazy<ILogger> _logger;
        private readonly Lazy<IEnumerable<IRequestStore>> _requests;
        private readonly Dictionary<Type, ICommandValidator> _validators = new Dictionary<Type, ICommandValidator>();
        private readonly Lazy<IExecutionContextResolver> _contextResolver;
        private readonly Lazy<IEventStream> _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorController"/> class.
        /// </summary>
        /// <param name="context">The configured <see cref="IComponentContext"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public ActorController(IComponentContext context)
        {
            Argument.NotNull(context, nameof(context));

            _context = context;
            _logger = new Lazy<ILogger>(() => _context.Resolve<ILogger>());
            _events = new Lazy<IEnumerable<IEventStore>>(() => _context.ResolveAll<IEventStore>());
            _requests = new Lazy<IEnumerable<IRequestStore>>(() => _context.ResolveAll<IRequestStore>());
            _contextResolver = new Lazy<IExecutionContextResolver>(() => _context.Resolve<IExecutionContextResolver>());
            _stream = new Lazy<IEventStream>(() => _context.Resolve<IEventStream>());
        }

        public async Task<CommandResult> Execute(IMessage command, IHandle handler, ExecutionContext context, TimeSpan? timeout = null)
        {
            Argument.NotNull(command, nameof(command));

            _logger.Value.Verbose("Starting execution for \"" + command.GetType().Name + "\" with \"" + handler.GetType().Name + "\" at \"" + context.Path + "\".");

            var events = context.RaisedEvents.ToList();

            // create the result
            var result = new CommandResult(context);
            result.Actor = handler.GetType().Name;

            try
            {
                // validate the command
                await this.ValidateCommand(command, result, context);

                if (!result.ValidationErrors.Any())
                {
                    // execute the handler
                    handler.SetContext(context);
                    var response = await handler.HandleAsync(command);
                    if (!(response is Task))
                    {
                        result.Response = response;
                    }
                    result.Actor = handler.GetType().Name;
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
            await this.Log(command, result, context, context.RaisedEvents.Except(events));

            return result;
        }

        protected virtual Task Log(IMessage command, CommandResult result, ExecutionContext context, IEnumerable<Event> events)
        {
            var tasks = _requests.Value.Select(e => e.AppendAsync(new RequestEntry(command, result, context))).ToList();
            foreach (var instance in events)
            {
                tasks.AddRange(_events.Value.Select(e => e.AppendAsync(new EventEntry(instance, context))));
            }

            var name = command.GetType().Name;
            if (!result.IsSuccessful)
            {
                if (result.RaisedException != null)
                {
                    _logger.Value.Error(result.RaisedException, "An unhandled exception was raised while executing " + name + ".", command);
                }
                else if (result.ValidationErrors?.Any() ?? false)
                {
                    _logger.Value.Verbose("Execution completed with validation errors while executing " + command + ": " + String.Join("; ", result.ValidationErrors.Select(e => e.ErrorType + ": " + e.Message)), command);
                }
                else
                {
                    _logger.Value.Error("Execution completed unsuccessfully while executing " + name + ".", command);
                }
            }
            else
            {
                _logger.Value.Verbose("Successfully completed " + name + ".", command);
            }

            return Task.WhenAll(tasks);
        }

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

        protected virtual void PublishEvents(IEnumerable<Event> events, ExecutionContext context)
        {
            foreach (var item in events)
            {
                _stream.Value.Publish(item, context);
            }
        }

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
    }
}