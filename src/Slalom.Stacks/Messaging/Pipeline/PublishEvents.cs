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
        private readonly IMessageGatewayAdapter _eventGatewayAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishEvents"/> class.
        /// </summary>
        /// <param name="components">The components.</param>
        public PublishEvents(IComponentContext components)
        {
            _eventGatewayAdapter = components.Resolve<IMessageGatewayAdapter>();
        }

        /// <inheritdoc />
        public Task Execute(IMessage message, MessageExecutionContext context)
        {
            if (context.IsSuccessful)
            {
                _eventGatewayAdapter.Publish(context.RaisedEvents, context);
                if (context.Response is IEvent)
                {
                    _eventGatewayAdapter.Publish((IEvent)context.Response, context);
                }
            }
            return Task.FromResult(0);
        }
    }
}