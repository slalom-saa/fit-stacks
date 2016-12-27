using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Communication
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
        /// <param name="context">The current execution context.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task Handle(TEvent instance, ExecutionContext context);
    }
}