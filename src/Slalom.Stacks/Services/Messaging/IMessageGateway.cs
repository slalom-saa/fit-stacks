using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// An adpater to a specific message gateway implementation like Akka.NET.
    /// </summary>
    public interface IMessageGateway
    {
        /// <summary>
        /// Publishes the specified event to any configured publish-subscribe endpoint.
        /// </summary>
        /// <param name="instance">The event instances.</param>
        /// <param name="context">The current context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Publish(EventMessage instance, ExecutionContext context = null);

        /// <summary>
        /// Publishes the specified events to any configured publish-subscribe endpoint.
        /// </summary>
        /// <param name="instances">The event instances.</param>
        /// <param name="context">The current context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Publish(IEnumerable<EventMessage> instances, ExecutionContext context = null);

        /// <summary>
        /// Publishes the specified events to any configured publish-subscribe endpoint.
        /// </summary>
        /// <param name="channel">The message channel.</param>
        /// <param name="message">The message to publish.</param>
        /// <returns>A task for asynchronous programming.</returns>
        void Publish(string channel, string message);

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="parentContext">The parent message context.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Send(object message, ExecutionContext parentContext = null, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="path">The path to the receiver.</param>
        /// <param name="parentContext">The parent message context.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Send(string path, ExecutionContext parentContext = null, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="path">The path to the receiver.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="parentContext">The parent message context.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Send(string path, object message, ExecutionContext parentContext = null, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="path">The path to the receiver.</param>
        /// <param name="command">The serialized command to send.</param>
        /// <param name="parentContext">The parent message context.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Send(string path, string command, ExecutionContext parentContext = null, TimeSpan? timeout = null);
    }
}