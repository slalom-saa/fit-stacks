using System;
using System.Collections.Generic;
using System.Security.Principal;
using Slalom.Stacks.Runtime;
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

        public IPrincipal User => _context.Request.User;

        /// <summary>
        /// Validates the specified message instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        public abstract IEnumerable<ValidationError> Validate(TCommand instance);

        public void UseContext(ExecutionContext context)
        {

            _context = context;
        }
    }
}