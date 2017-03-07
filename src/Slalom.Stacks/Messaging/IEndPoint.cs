using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IEndPoint<TMessage>
    {
        /// <summary>
        /// Handles the specified message instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Receive(TMessage instance);
    }

    public interface IEndPoint<TRequest, TResponse>
    {
        /// <summary>
        /// Handles the specified message instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<TResponse> Handle(TRequest instance);
    }
}