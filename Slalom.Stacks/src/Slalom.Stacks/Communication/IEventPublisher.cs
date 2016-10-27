using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Defines an <see href="http://bit.ly/2dRwOXq">Event Publisher</see>, responsible for locating event handlers and executing multi-threaded and/or out-of-process flow.
    /// </summary>
    /// <seealso cref="Event"/>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="instance">The event to publish.</param>
        /// <param name="context">The current context.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task Publish<TEvent>(TEvent instance, ExecutionContext context) where TEvent : IEvent;
    }
}