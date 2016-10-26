using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Handles commands of the specified type.  The result type should either be an event or a document.
    /// </summary>
    /// <typeparam name="TCommand">The type of command.</typeparam>
    /// <typeparam name="TResult">The type of result.  Either an event or a message.</typeparam>
    /// <seealso href="http://bit.ly/2eajcKW">Enterprise Integration Patterns: Designing, Building, and Deploying Messaging Solutions</seealso>
    public interface ICommandHandler<in TCommand, TResult> where TCommand : Command<TResult>
    {
        /// <summary>
        /// Gets or sets the current <seealso cref="ExecutionContext"/>.
        /// </summary>
        /// <value>The current <seealso cref="ExecutionContext"/>.</value>
        ExecutionContext Context { get; set; }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<TResult> Handle(TCommand command);
    }
}