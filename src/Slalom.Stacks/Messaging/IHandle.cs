using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// The root IHandle interface.
    /// </summary>
    /// <typeparam name="TMessage">The message type.</typeparam>
    public interface IHandle<in TMessage>
    {
        /// <summary>
        /// Handles the specified message instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Handle(TMessage instance);
    }
}