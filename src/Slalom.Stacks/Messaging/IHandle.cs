using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Defines a method for handling a message.
    /// </summary>
    public interface IHandle
    {
        /// <summary>
        /// Handles the specified message instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Handle(IMessage instance);
    }
}