using System;
using System.Collections.Generic;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Performs security validation on a command.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    public abstract class SecurityRule<TCommand> : ISecurityValidationRule<TCommand> where TCommand : IMessage
    {
        /// <summary>
        /// Gets the execution context.
        /// </summary>
        /// <value>The execution context.</value>
        public ExecutionContext Context { get; private set; }

        /// <summary>
        /// Checks to see if the current user is in the specified role.
        /// </summary>
        /// <param name="role">The role to check for.</param>
        /// <returns><c>true</c> if the current user is in the specified role, <c>false</c> otherwise.</returns>
        public bool UserInRole(string role)
        {
            return this.Context.User?.IsInRole(role) ?? false;
        }

        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
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
        protected abstract IEnumerable<ValidationError> Validate(TCommand instance);
    }
}