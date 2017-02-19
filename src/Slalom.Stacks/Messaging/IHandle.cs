using System;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Handles a message of the specified type.
    /// </summary>
    public interface IHandle
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="instance">The message instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<object> Handle(MessageEnvelope instance);
   }

    public interface IHandle<TMessage> : IHandle
    {
    }
}