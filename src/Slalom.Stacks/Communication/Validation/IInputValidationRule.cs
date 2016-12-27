using System;
using System.Collections.Generic;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication.Validation
{
    /// <summary>
    /// Defines a contract for input validation of a command.
    /// </summary>
    /// <typeparam name="TValue">The type of the command to validate.</typeparam>
    public interface IInputValidationRule<in TValue> where TValue : ICommand
    {
        /// <summary>
        /// Validates the specified instance and returns any validation errors.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="context">The current context that can be used to share information
        /// between validation rules.</param>
        /// <returns>Returns all found validation errors.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        IEnumerable<ValidationError> Validate(TValue instance, ExecutionContext context);
    }
}