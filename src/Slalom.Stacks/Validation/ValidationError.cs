using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Slalom.Stacks.Validation
{
    /// <summary>
    /// Represents a validation error and contains user facing messaging.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        public ValidationError()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="message">The message text.</param>
        public ValidationError(string message)
            : this(null, message, ValidationErrorType.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="type">The validation type.</param>
        public ValidationError(string message, ValidationErrorType type)
            : this(null, message, type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="code">The message code for client consumption.</param>
        /// <param name="message">The message text.</param>
        public ValidationError(string code, string message)
            : this(code, message, ValidationErrorType.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="code">The message code for client consumption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">The validation type.</param>
        public ValidationError(string code, string message, ValidationErrorType type)
        {
            this.Code = code;
            this.Message = message;
            this.ErrorType = type;
        }

        /// <summary>
        /// Gets the message code for client consumption.
        /// </summary>
        /// <value>
        /// The message code.
        /// </value>
        public string Code { get; private set; }

        /// <summary>
        /// Gets validation type.
        /// </summary>
        /// <value>
        /// The validation type.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidationErrorType ErrorType { get; private set; }

        /// <summary>
        /// Gets the message text.
        /// </summary>
        /// <value>
        /// The message text.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="ValidationError"/>.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ValidationError(string message)
        {
            return new ValidationError(message);
        }

        /// <summary>
        /// Adds the type to the message.
        /// </summary>
        /// <param name="type">The type of validation.</param>
        /// <returns>The current instance.</returns>
        public ValidationError WithType(ValidationErrorType type)
        {
            this.ErrorType = type;

            return this;
        }
    }
}