using System;
using System.Linq;

namespace Slalom.Stacks.Validation
{
    /// <summary>
    /// The exception that is raised when a validation error is found.
    /// </summary>
    /// <seealso cref="System.InvalidOperationException" />
    public class ValidationException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="errors">The validation errors to add to the exception.</param>
        public ValidationException(params ValidationError[] errors)
            : base(string.Join(Environment.NewLine, errors.Select(e => e.ToString())))
        {
            this.ValidationErrors = errors;
        }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>The validation messages.</value>
        public ValidationError[] ValidationErrors { get; private set; }
    }
}