using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Defines an Application Message Bus.  An Application Message bus is an addition to and not a replacement to a typical Service Bus.  
    /// It runs in memory and is intended to send only to in-memory receivers.
    /// </summary>
    /// <seealso href="[Documentation URL]"/>
    public class MessageBus : IMessageBus
    {
        private readonly ICommandCoordinator _coordinator;
        private readonly IExecutionContextResolver _executionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBus"/> class.
        /// </summary>
        /// <param name="executionContext">The current <see cref="IExecutionContextResolver"/> instance.</param>
        /// <param name="coordinator">The current <see cref="ICommandCoordinator"/> instance.</param>
        public MessageBus(IExecutionContextResolver executionContext, ICommandCoordinator coordinator)
        {
            _executionContext = executionContext;
            _coordinator = coordinator;
        }

        /// <summary>
        /// Sends the specified command to the service bus and attaches a <seealso cref="IExecutionContextResolver">context</seealso> before multi-threading.
        /// </summary>
        /// <typeparam name="TResult">The return type of the command.</typeparam>
        /// <param name="command">The command to send and execute.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task<CommandResult<TResult>> Send<TResult>(Command<TResult> command)
        {
            return await _coordinator.Handle(command, _executionContext.Resolve());
        }
    }
}