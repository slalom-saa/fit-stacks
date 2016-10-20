using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging.Validation
{
    /// <summary>
    /// Extends validation classes with additional methods.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Adds the specified type all instances in teh specified collection.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="type">The error type.</param>
        /// <returns>A collection containing all of the errors with the correct type set.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        public static IEnumerable<ValidationError> WithType(this IEnumerable<ValidationError> instance, ValidationErrorType type)
        {
            Argument.NotNull(() => instance);

            return instance.Select(e => e.WithType(type));
        }
    }
}