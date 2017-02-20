using System;
using System.Collections.Generic;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Represents an input validation rule set that should be run as a single unit.
    /// </summary>
    /// <typeparam name="TMessage">The type of message.</typeparam>
    public abstract class InputRule<TMessage> : IInputValidationRule<TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Gets the execution context.
        /// </summary>
        /// <value>The execution context.</value>
        public ExecutionContext Context { get; private set; }

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

            return this.Validate((TMessage)instance.Message);
        }

        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        protected abstract IEnumerable<ValidationError> Validate(TMessage instance);
    }
}