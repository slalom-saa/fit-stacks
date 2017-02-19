using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Performs business validation on a command.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <seealso cref="IBusinessValidationRule{TCommand}" />
    public abstract class BusinessRule<TCommand> : IBusinessValidationRule<TCommand> where TCommand : IMessage
    {
        /// <summary>
        /// Gets the execution context.
        /// </summary>
        /// <value>The execution context.</value>
        public ExecutionContext Context { get; private set; }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/> instance.</value>
        public IDomainFacade Domain { get; set; }

        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance" /> argument is null.</exception>
        public IEnumerable<ValidationError> Validate(MessageEnvelope instance)
        {
            Argument.NotNull(instance, nameof(instance));

            this.Context = instance.Context;

            return this.Validate(instance);
        }

        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        /// <exception cref="System.NotImplementedException">Thrown when neither validate methods are implemented.</exception>
        protected virtual IEnumerable<ValidationError> Validate(TCommand instance)
        {
            Argument.NotNull(instance, nameof(instance));

            var result = this.ValidateAsync(instance).Result;
            if (result == null)
            {
                return Enumerable.Empty<ValidationError>();
            }
            return new[] { result };
        }


        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.NotImplementedException">Thrown when neither validate methods are implemented.</exception>
        public virtual Task<ValidationError> ValidateAsync(TCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}