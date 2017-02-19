using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Exceptions;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Supervises the execution and completion of commands.  Returns a result containing the returned value if the command is successful; 
    /// otherwise, returns information about why the execution was not successful.
    /// </summary>
    public class ActorSupervisor
    {
        private readonly IComponentContext _context;
        private readonly Lazy<IExecutionExceptionHandler> _exceptions;
        private readonly Lazy<ICommandLogger> _logger;
        private readonly Lazy<IEventStream> _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorSupervisor"/> class.
        /// </summary>
        /// <param name="context">The configured <see cref="IComponentContext"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public ActorSupervisor(IComponentContext context)
        {
            Argument.NotNull(context, nameof(context));

            _context = context;
            _logger = new Lazy<ICommandLogger>(() => _context.Resolve<ICommandLogger>());
            _stream = new Lazy<IEventStream>(() => _context.Resolve<IEventStream>());
            _exceptions = new Lazy<IExecutionExceptionHandler>(() => _context.Resolve<IExecutionExceptionHandler>());
        }

        public virtual async Task<MessageExecutionResult> Execute(MessageEnvelope instance, IHandle handler, TimeSpan? timeout = null)
        {
            await _logger.Value.LogStart(instance, handler);

            var context = instance.Context;
            var message = instance.Message;

            // create the result
            var result = new MessageExecutionResult(context);
            result.Handler = handler.GetType().Name;

            try
            {
                // validate the command
                var target = (ICommandValidator)_context.Resolve(typeof(CommandValidator<>).MakeGenericType(message.GetType()));
                result.AddValidationErrors(await target.Validate(instance));

                if (!result.ValidationErrors.Any())
                {
                    // execute the handler
                    var response = await handler.Handle(instance);
                    if (!(response is Task))
                    {
                        result.Response = response;
                    }
                }

                // publish all events
                if (instance.Message is Command)
                {
                    foreach (var item in instance.Context.RaisedEvents)
                    {
                        _stream.Value.Publish(item, context);
                    }
                }
            }
            catch (Exception exception)
            {
                _exceptions.Value.HandleException(instance, result, exception);
            }

            // finalize the result and mark it as complete
            result.Complete();

            await _logger.Value.LogCompletion(instance, result, handler);

            return result;
        }
    }
}