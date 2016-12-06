using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Communication.Logging
{
    /// <summary>
    /// Defines a contract for storing logs.  A log is a history of commands that were executed.  Diagnostic logging is done with
    /// the <see cref="ILogger"/>.
    /// </summary>
    public interface ILogStore
    {
        /// <summary>
        /// Appends an audit with the specified execution elements.
        /// </summary>
        /// <param name="command">The command that was executed.</param>
        /// <param name="result">The execution result.</param>
        /// <param name="context">The current <see cref="T:Slalom.Boost.Commands.CommandContext" /> instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task AppendAsync(ICommand command, ICommandResult result, ExecutionContext context);
    }
}