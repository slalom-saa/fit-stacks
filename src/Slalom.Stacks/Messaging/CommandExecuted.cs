using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    public class CommandExecuted
    {
        private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecuted"/> class.
        /// </summary>
        /// <param name="context">The current context.</param>
        public CommandExecuted(ExecutionContext context)
        {
            this.CorrelationId = context.CorrelationId;
            this.Started = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Gets or sets the date and time completed.
        /// </summary>
        /// <value>The date and time completed.</value>
        public DateTimeOffset? Completed { get; private set; }

        /// <summary>
        /// Gets the time elapsed.
        /// </summary>
        /// <value>The time elapsed.</value>
        public TimeSpan? Elapsed => this.Completed - this.Started;

        /// <summary>
        /// Gets or sets the date and time started.
        /// </summary>
        /// <value>The date and time started.</value>
        public DateTimeOffset Started { get; private set; }

        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        public string CorrelationId { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the execution was successful.
        /// </summary>
        /// <value><c>true</c> if the execution was successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful => !_validationErrors.Any() && this.RaisedException == null;

        /// <summary>
        /// Gets the raised exception if any.
        /// </summary>
        /// <value>The raised exception.</value>
        public Exception RaisedException { get; private set; }

        /// <summary>
        /// Gets any validation errors that were raised.
        /// </summary>
        /// <value>The validation errors that were raised.</value>
        public IEnumerable<ValidationError> ValidationErrors => _validationErrors.AsEnumerable();

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

        /// <summary>
        /// Completes this instance.
        /// </summary>
        public void Complete()
        {
            this.Completed = DateTimeOffset.UtcNow;
        }

        public object Response { get; private set; }

        public void AddResponse(object o)
        {
            Response = o;
        }
    }
}