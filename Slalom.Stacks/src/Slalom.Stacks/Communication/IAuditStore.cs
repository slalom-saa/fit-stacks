using System;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Defines a contract for storing audits.
    /// </summary>
    public interface IAuditStore
    {
        /// <summary>
        /// Appends an audit with the specified execution elements.
        /// </summary>
        /// <param name="command">The command that was executed.</param>
        /// <param name="result">The command result.</param>
        /// <param name="context">The current <see cref="T:Slalom.Boost.Commands.CommandContext" /> instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task AppendAsync(ICommand command, ICommandResult result, ExecutionContext context);
    }
}