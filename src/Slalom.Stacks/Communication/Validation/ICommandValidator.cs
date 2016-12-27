using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication.Validation
{
    /// <summary>
    /// Validates commands using input, security and business rules.
    /// </summary>
    public interface ICommandValidator
    {
        /// <summary>
        /// Validates the specified command.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>The <see cref="ValidationError">messages</see> returned from validation routines.</returns>
        Task<IEnumerable<ValidationError>> Validate(ICommand command, ExecutionContext context);
    }
}