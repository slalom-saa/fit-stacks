using System.Threading.Tasks;
using Slalom.FitStacks.Runtime;

namespace Slalom.FitStacks.Messaging
{
    /// <summary>
    /// Defines a contract for handling events of the specified type.
    /// </summary>
    /// <typeparam name="TEvent">The type of event.</typeparam>
    /// <seealso cref="Event"/>
    public interface IHandleEvent<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Handles the specified event instance.
        /// </summary>
        /// <param name="instance">The event instance.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task Handle(TEvent instance, ExecutionContext context);
    }

    /// <summary>
    /// Defines a contract for handling all events.
    /// </summary>
    public interface IHandleEvent
    {
        /// <summary>
        /// Handles the specified event instance.
        /// </summary>
        /// <param name="instance">The event instance.</param>
        /// <param name="context">The current execution instance.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task Handle(Event instance, ExecutionContext context);
    }
}