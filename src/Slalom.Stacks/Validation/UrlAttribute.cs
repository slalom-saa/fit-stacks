using System;

namespace Slalom.Stacks.Validation
{
    /// <summary>
    /// Validates that a property is a well-formed URL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UrlAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlAttribute" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UrlAttribute(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            if (value is Uri)
            {
                return true;
            }
            if (value is string)
            {
                var strInput = ((string) value).Trim();
                return Uri.IsWellFormedUriString(strInput, UriKind.RelativeOrAbsolute);
            }
            return false;
        }
    }
}