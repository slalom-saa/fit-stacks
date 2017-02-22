using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Validation
{
    /// <summary>
    /// Defines a contract for validating a class instance.
    /// </summary>
    /// <typeparam name="TValue">The instance type to validate.</typeparam>
    public interface IValidationRule<in TValue>
    {
        /// <summary>
        /// Validates the specified instance and returns any validation errors.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>Returns all found validation errors.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        IEnumerable<ValidationError> Validate(TValue instance);
    }
}