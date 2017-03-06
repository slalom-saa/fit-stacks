using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Persistence;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The publish events step of the usecase execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class PublishEvents : IMessageExecutionStep
    {
        private readonly IMessageGateway _messageGateway;
        private IEventStore _eventStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishEvents"/> class.
        /// </summary>
        /// <param name="components">The components.</param>
        public PublishEvents(IComponentContext components)
        {
            _messageGateway = components.Resolve<IMessageGateway>();
            _eventStore = components.Resolve<IEventStore>();
        }

        /// <inheritdoc />
        public async Task Execute(IMessage message, ExecutionContext context)
        {
            if (context.IsSuccessful)
            {
                foreach (var instance in context.RaisedEvents.Union(new[] { context.Response as Event }).Where(e => e != null))
                {
                    await _eventStore.Append(instance);

                    await _messageGateway.Publish(instance, context);
                }
            }
        }
    }
}