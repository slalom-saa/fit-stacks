using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication.Validation
{
    /// <summary>
    /// A rule that checks to make sure the current user is in a specific role.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to validate.</typeparam>
    /// <seealso cref="Slalom.Stacks.Communication.Validation.SecurityValidationRule{TCommand}" />
    public class InRoleSecurityRule<TCommand> : SecurityValidationRule<TCommand> where TCommand : ICommand
    {
        private readonly ValidationError _error;
        private readonly string _role;

        /// <summary>
        /// Initializes a new instance of the <see cref="InRoleSecurityRule{TCommand}"/> class.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <param name="error">The validation to return when the user is not in the role.</param>
        public InRoleSecurityRule(string role, ValidationError error)
        {
            _role = role;
            _error = error;
        }

        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected override Task<ValidationError> Validate(TCommand instance)
        {
            if (!this.Context.User?.HasClaim(ClaimTypes.Role, _role) ?? true)
            {
                return Task.FromResult(_error);
            }
            return null;
        }
    }
}