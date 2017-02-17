using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Defines an Event Stream.
    /// </summary>
    /// <seealso cref="Event"/>
    public interface IEventStream
    {
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="instance">The event to publish.</param>
        /// <param name="context">The current context.</param>
        void Publish<TEvent>(TEvent instance, ExecutionContext context) where TEvent : IEvent;
    }
}