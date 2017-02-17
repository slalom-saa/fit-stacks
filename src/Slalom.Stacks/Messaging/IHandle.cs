using System;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// The root IHandle interface, used for registration.
    /// </summary>
    public interface IHandle
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="instance">The message instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<object> HandleAsync(object instance);

        /// <summary>
        /// Sets the current <see cref="ExecutionContext"/>.
        /// </summary>
        /// <param name="context">The current <see cref="ExecutionContext"/>.</param>
        void SetContext(ExecutionContext context);
    }

    /// <summary>
    /// A typed IHandle interface, used for registration.
    /// </summary>
    /// <typeparam name="TMessage">The message type.</typeparam>
    /// <seealso cref="Slalom.Stacks.Messaging.IHandle" />
    public interface IHandle<TMessage> : IHandle
    {
    }
}