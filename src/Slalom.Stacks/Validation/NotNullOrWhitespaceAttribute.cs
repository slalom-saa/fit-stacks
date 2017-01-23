using System;

namespace Slalom.Stacks.Validation
{
    /// <summary>
    /// Validates that a property is not null or whitespace.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Validation.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullOrWhitespaceAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullOrWhitespaceAttribute"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NotNullOrWhitespaceAttribute(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Returns true if the object value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }
    }
}