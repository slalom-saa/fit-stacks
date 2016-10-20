using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.FitStacks.Logging;
using Slalom.FitStacks.Runtime;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging
{
    /// <summary>
    /// Represents the result of command execution and contains the value returned by the handler.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned from the handler.</typeparam>
    public class CommandResult<TResult>
    {
        private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult{TResult}"/> class.
        /// </summary>
        /// <param name="context">The execution context.</param>
        public CommandResult(ExecutionContext context)
        {
            this.CorrelationId = context.CorrelationId;
        }

        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        public string CorrelationId { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the execution was successful.
        /// </summary>
        /// <value><c>true</c> if the execution was successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful => !_validationErrors.Any() && this.RaisedException == null;

        /// <summary>
        /// Gets the raised exception if any.
        /// </summary>
        /// <value>The raised exception.</value>
        public Exception RaisedException { get; protected set; }

        /// <summary>
        /// Gets any validation errors that were raised.
        /// </summary>
        /// <value>The validation errors that were raised.</value>
        public IEnumerable<ValidationError> ValidationErrors => _validationErrors.AsEnumerable();

        /// <summary>
        /// Gets or sets the value returned by the handler.
        /// </summary>
        /// <value>The value returned by the handler.</value>
        [Ignore]
        public TResult Value { get; set; }

        /// <summary>
        /// Adds the exception to the raised exceptions.
        /// </summary>
        /// <param name="exception">The exception to add.</param>
        public void AddException(Exception exception)
        {
            this.RaisedException = exception;
        }

        /// <summary>
        /// Adds the specified validation errors to the result.
        /// </summary>
        /// <param name="validationErrors">The validation errors to add.</param>
        public void AddValidationErrors(IEnumerable<ValidationError> validationErrors)
        {
            _validationErrors.AddRange(validationErrors);
        }
    }
}