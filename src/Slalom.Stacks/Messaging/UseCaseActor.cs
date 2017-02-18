using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Defines a use case actor that performs a defined function.
    /// </summary>
    /// <typeparam name="TCommand">The type of command.</typeparam>
    /// <typeparam name="TResult">The type of result.</typeparam>
    /// <seealso cref="Slalom.Stacks.Messaging.IHandle{TCommand}" />
    public abstract class UseCaseActor<TCommand, TResult> : IHandle<TCommand> where TCommand : ICommand
    {
        private IDomainFacade _domain;

        /// <summary>
        /// Gets the current <see cref="ExecutionContext"/>.
        /// </summary>
        /// <value>The current <see cref="ExecutionContext"/>.</value>
        public ExecutionContext Context { get; private set; }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/>.</value>
        public IDomainFacade Domain
        {
            get { return _domain.SetContext(this.Context); }
            set { _domain = value; }
        }

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/>.</value>
        public ISearchFacade Search { get; set; }

        /// <summary>
        /// Executes the use case given the specified command.
        /// </summary>
        /// <param name="command">The command containing the input.</param>
        /// <returns>The command result.</returns>
        public virtual TResult Execute(TCommand command)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} use case actor have not been implemented.");
        }

        /// <summary>
        /// Executes the use case given the specified command.
        /// </summary>
        /// <param name="command">The command containing the input.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task<TResult> ExecuteAsync(TCommand command)
        {
            return Task.FromResult(this.Execute(command));
        }

        /// <summary>
        /// Sets the current <see cref="ExecutionContext"/>.
        /// </summary>
        /// <param name="context">The current <see cref="ExecutionContext"/>.</param>
        void IHandle.SetContext(ExecutionContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Validates the specified command.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual IEnumerable<ValidationError> Validate(TCommand command)
        {
            yield break;
        }

        /// <summary>
        /// Validates the specified command.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual Task<IEnumerable<ValidationError>> ValidateAsync(TCommand command)
        {
            return Task.FromResult(this.Validate(command));
        }

        async Task<object> IHandle.HandleAsync(object instance)
        {
            var result = await this.ExecuteAsync((TCommand)instance);

            return result;
        }
    }
}