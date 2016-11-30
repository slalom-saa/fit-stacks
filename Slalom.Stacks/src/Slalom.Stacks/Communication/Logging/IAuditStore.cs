using System;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Communication.Logging
{
    /// <summary>
    /// Defines a contract for storing audits.
    /// </summary>
    public interface IAuditStore
    {
        /// <summary>
        /// Appends an audit with the specified execution elements.
        /// </summary>
        /// <param name="event">The raised event.</param>
        /// <param name="context">The current <see cref="T:Slalom.Boost.Commands.CommandContext" /> instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task AppendAsync(IEvent @event, ExecutionContext context);
    }
}