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
    /// <typeparam name="TEvent">The type of command.</typeparam>
    public abstract class Actor<TEvent> : IHandle<TEvent> where TEvent : Event
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
        /// <param name="message">The command containing the input.</param>
        /// <returns>The command result.</returns>
        public virtual void Execute(TEvent message)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} actor have not been implemented.");
        }

        /// <summary>
        /// Executes the use case given the specified command.
        /// </summary>
        /// <param name="message">The command containing the input.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task ExecuteAsync(TEvent message)
        {
            this.Execute(message);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Validates the specified command.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual IEnumerable<ValidationError> Validate(TEvent command)
        {
            yield break;
        }

        /// <summary>
        /// Validates the specified command.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual Task<IEnumerable<ValidationError>> ValidateAsync(TEvent command)
        {
            return Task.FromResult(this.Validate(command));
        }

        public async Task<object> Handle(MessageEnvelope instance)
        {
            this.Context = instance.Context;

            await this.ExecuteAsync((TEvent)instance.Message);

            return null;
        }
    }
}