using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An adpater to a specific message gateway implementation like Akka.NET.
    /// </summary>
    public interface IMessageGatewayAdapter
    {
        /// <summary>
        /// Publishes the specified event to the configured publish-subscribe endpoint.
        /// </summary>
        /// <param name="instance">The event instances.</param>
        /// <param name="context">The current context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Publish(IEvent instance, MessageExecutionContext context = null);

        /// <summary>
        /// Publishes the specified events to the configured publish-subscribe endpoint.
        /// </summary>
        /// <param name="instances">The event instances.</param>
        /// <param name="context">The current context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Publish(IEnumerable<IEvent> instances, MessageExecutionContext context = null);

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="parentContext">The parent message context.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Send(ICommand command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="path">The path to the receiver.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="parentContext">The parent message context.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Send(string path, ICommand command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="path">The path to the receiver.</param>
        /// <param name="command">The serialized command to send.</param>
        /// <param name="parentContext">The parent message context.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Send(string path, string command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null);
    }
}