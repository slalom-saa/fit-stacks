using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The publish events step of the usecase execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class PublishEvents : IMessageExecutionStep
    {
        private readonly IMessageGateway _eventGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishEvents"/> class.
        /// </summary>
        /// <param name="components">The components.</param>
        public PublishEvents(IComponentContext components)
        {
            _eventGateway = components.Resolve<IMessageGateway>();
        }

        /// <inheritdoc />
        public Task Execute(IMessage message, MessageExecutionContext context)
        {
            if (context.IsSuccessful)
            {
                _eventGateway.Publish(context.RaisedEvents, context);
                if (context.Response is IEvent)
                {
                    _eventGateway.Publish((IEvent)context.Response, context);
                }
            }
            return Task.FromResult(0);
        }
    }
}