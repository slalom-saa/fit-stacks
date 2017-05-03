using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// Publishes events to an external subscriber.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publishes the specified events.
        /// </summary>
        /// <param name="events">The events to publish.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Publish(params EventMessage[] events);
    }
}