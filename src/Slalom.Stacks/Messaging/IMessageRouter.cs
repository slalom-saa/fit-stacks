using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Routes messaging from external sources to internal actors.
    /// </summary>
    public interface IMessageRouter
    {
        /// <summary>
        /// Sends the specified message with the specified timeout.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageExecutionResult> Send(IMessage message, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified message with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageExecutionResult> Send(string path, IMessage message, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified message with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageExecutionResult> Send(string path, string message, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified message with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageExecutionResult> Send(string path, TimeSpan? timeout);
    }
}