using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Defines a Command Coordinator.
    /// </summary>
    public interface ICommandCoordinator
    {
        /// <summary>
        /// Sends the specified command with the specified timeout.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<CommandResult> SendAsync(IMessage command, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified command with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<CommandResult> SendAsync(string path, IMessage command, TimeSpan? timeout = null);

        /// <summary>
        /// Sends the specified command with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null);
    }
}
