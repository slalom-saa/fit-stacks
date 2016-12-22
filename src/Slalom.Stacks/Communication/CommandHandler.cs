using System;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Handles commands of the specified type.  The result type should either be an event or a document.
    /// </summary>
    /// <typeparam name="TCommand">The type of command.</typeparam>
    /// <typeparam name="TResult">The type of result.  Either an event or a message.</typeparam>
    /// <seealso cref="ICommandHandler{TCommand,TResult}" />
    /// <seealso href="http://bit.ly/2eajcKW">Enterprise Integration Patterns: Designing, Building, and Deploying Messaging Solutions</seealso>
    public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>, IHaveDomainFacade,
                                                              IHaveSearchFacade, IHaveExecutionContext where TCommand : Command<TResult>
    {
        /// <summary>
        /// Gets or sets the current <seealso cref="ExecutionContext"/>.
        /// </summary>
        /// <value>The current <seealso cref="ExecutionContext"/>.</value>
        public ExecutionContext Context { get; set; }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task<TResult> HandleAsync(TCommand command)
        {
            return Task.FromResult(this.Handle(command));
        }

        /// <summary>
        /// Gets or sets the configured <see cref="IDomainFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/>.</value>
        public IDomainFacade Domain { get; protected set; }

        /// <summary>
        /// Gets or sets the configured <see cref="ISearchFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/>.</value>
        public ISearchFacade Search { get; protected set; }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The execution result.</returns>
        /// <exception cref="System.NotImplementedException">Thrown if no handling method is implemented</exception>
        public virtual TResult Handle(TCommand command)
        {
            throw new NotImplementedException();
        }
    }
}