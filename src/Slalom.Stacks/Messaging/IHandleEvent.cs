using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Handles events of the specified type.
    /// </summary>
    /// <typeparam name="TEvent">The type of event.</typeparam>
    /// <seealso cref="IEvent"/>
    public interface IHandleEvent<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Handles the specified event instance.
        /// </summary>
        /// <param name="instance">The event instance.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task HandleAsync(TEvent instance);
    }

    /// <summary>
    /// Handles events of the specified type.
    /// </summary>
    /// <seealso cref="IEvent"/>
    public interface IHandleEvent
    {
        /// <summary>
        /// Handles the specified event instance.
        /// </summary>
        /// <param name="instance">The event instance.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task HandleAsync(IEvent instance);
    }
}