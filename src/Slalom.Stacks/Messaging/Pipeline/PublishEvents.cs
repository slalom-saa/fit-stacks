using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The publish events step of the Service execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class PublishEvents : IMessageExecutionStep
    {
        private readonly IEventStore _eventStore;
        private readonly IMessageGateway _messageGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishEvents" /> class.
        /// </summary>
        /// <param name="components">The current component context.</param>
        public PublishEvents(IComponentContext components)
        {
            _messageGateway = components.Resolve<IMessageGateway>();
            _eventStore = components.Resolve<IEventStore>();
        }

        /// <inheritdoc />
        public async Task Execute(ExecutionContext context)
        {
            if (context.IsSuccessful)
            {
                foreach (var instance in context.RaisedEvents.Union(new[] {context.Response as EventMessage}).Where(e => e != null))
                {
                    await _eventStore.Append(instance);

                    await _messageGateway.Publish(instance, context);
                }
            }
        }
    }
}