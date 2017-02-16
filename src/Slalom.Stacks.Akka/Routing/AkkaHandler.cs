using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    public class AkkaHandler<TUseCase> : ReceiveActor where TUseCase : IHandle
    {
        private Lazy<ILogger> _logger;
        private Lazy<IEventStream> _publisher;
        private Lazy<IEnumerable<IAuditStore>> _audits;
        private Lazy<IEnumerable<IRequestStore>> _requests;
        private Lazy<IExecutionContextResolver> _contextResolver;
        public TUseCase UseCase { get; }
        public IComponentContext Context { get; }

        public AkkaHandler(TUseCase useCase, IComponentContext context)
        {
            this.UseCase = useCase;
            this.Context = context;

            _logger = new Lazy<ILogger>(context.Resolve<ILogger>);
            _publisher = new Lazy<IEventStream>(context.Resolve<IEventStream>);
            _audits = new Lazy<IEnumerable<IAuditStore>>(context.ResolveAll<IAuditStore>);
            _requests = new Lazy<IEnumerable<IRequestStore>>(context.ResolveAll<IRequestStore>);
            _contextResolver = new Lazy<IExecutionContextResolver>(context.Resolve<IExecutionContextResolver>);

            this.ReceiveAsync<ICommand>(this.HandleCommand);

            this.Receive<IEvent>(e =>
            {

            });
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
            var target = (ICommandValidator)this.Context.Resolve(typeof(CommandValidator<>).MakeGenericType(command.GetType()));

            var errors = await target.Validate(command, context);

            result.AddValidationErrors(errors);
        }

        private async Task HandleCommand(ICommand command)
        {
            _logger.Value.Verbose("Starting execution for " + command.Type + " at \"{Path}\". {@Command}", this.Self.Path, command);

            // get the context
            var context = _contextResolver.Value.Resolve();

            var result = new CommandResult(context);

            try
            {
                // validate the command
                await this.ValidateCommand(command, result, context);

                if (!result.ValidationErrors.Any())
                {
                    // execute the handler
                    var response = await this.UseCase.HandleAsync(command);
                    result.AddResponse(response);

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

            this.Sender.Tell(result);
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
            else if (exception is AggregateException && ((AggregateException)exception).InnerExceptions.Count == 1)
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
    }
}