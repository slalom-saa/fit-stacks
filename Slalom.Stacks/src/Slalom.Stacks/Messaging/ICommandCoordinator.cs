using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Supervises the execution and completion of commands.  Returns a result containing the returned value if the command is successful; 
    /// otherwise, returns information about why the execution was not succesful.
    /// </summary>
    public interface ICommandCoordinator
    {
        /// <summary>
        /// Handles the command, progressing it through the stages of the pipeline.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<CommandResult<TResult>> Handle<TResult>(Command<TResult> command, ExecutionContext context);
    }
}