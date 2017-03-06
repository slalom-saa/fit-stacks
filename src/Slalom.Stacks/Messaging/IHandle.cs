using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IHandle
    {
        Task Handle();
    }

    /// <summary>
    /// Defines a method for handling a message.
    /// </summary>
    public interface IHandle<TMessage>
    {
        /// <summary>
        /// Handles the specified message instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Handle(TMessage instance);
    }

    public interface IHandle<TRequest, TResponse>
    {
        /// <summary>
        /// Handles the specified message instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<TResponse> Handle(TRequest instance);
    }
}