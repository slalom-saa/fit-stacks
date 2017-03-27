using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Performs security validation on a message.
    /// </summary>
    /// <typeparam name="TCommand">The message type.</typeparam>
    public abstract class SecurityRule<TCommand> : ISecurityRule<TCommand>, IUseExecutionContext
    {
        private ExecutionContext _context;

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public IPrincipal User => _context.Request.User;

        /// <summary>
        /// Validates the specified message instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        /// <exception cref="System.NotImplementedException">Thrown when neither validate methods are implemented.</exception>
        public virtual IEnumerable<ValidationError> Validate(TCommand instance)
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
        /// Validates the specified message instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.NotImplementedException">Thrown when neither validate methods are implemented.</exception>
        public virtual Task<ValidationError> ValidateAsync(TCommand instance)
        {
            throw new NotImplementedException();
        }

        void IUseExecutionContext.UseContext(ExecutionContext context)
        {
            _context = context;
        }
    }
}