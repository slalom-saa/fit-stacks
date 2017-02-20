using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// The result of message execution.  Contains information about the execution and the response from
    /// the responding actor.
    /// </summary>
    public class MessageExecutionResult
    {
        private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageExecutionResult" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="context">The context.</param>
        public MessageExecutionResult(IMessage message, IHandle handler, ExecutionContext context)
        {
            this.MessageId = message.Id;
            this.CorrelationId = context.CorrelationId;
            this.Started = DateTimeOffset.UtcNow;
            this.Handler = handler.GetType().Name;
        }

        /// <summary>
        /// Gets the actor that handled the message.
        /// </summary>
        /// <value>The actor that handled the message.</value>
        public string Handler { get; private set; }

        /// <summary>
        /// Gets or sets the date and time that the execution completed.
        /// </summary>
        /// <value>The date and time that the execution completed.</value>
        public DateTimeOffset? Completed { get; private set; }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; private set; }

        /// <summary>
        /// Gets the time elapsed during the execution.
        /// </summary>
        /// <value>The time elapsed during the execution.</value>
        public TimeSpan? Elapsed => this.Completed - this.Started;

        /// <summary>
        /// Gets or sets a value indicating whether the execution was successful.
        /// </summary>
        /// <value><c>true</c> if the execution was successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful => !_validationErrors.Any() && this.RaisedException == null;

        /// <summary>
        /// Gets the raised exception, if any.
        /// </summary>
        /// <value>The raised exception.</value>
        public Exception RaisedException { get; set; }

        /// <summary>
        /// Gets the message response, if any.
        /// </summary>
        public object Response { get; set; }

        /// <summary>
        /// Gets the date and time the execution started.
        /// </summary>
        /// <value>The date and time the execution started.</value>
        public DateTimeOffset Started { get; private set; }

        /// <summary>
        /// Gets any validation errors that were raised.
        /// </summary>
        /// <value>The validation errors that were raised.</value>
        public IEnumerable<ValidationError> ValidationErrors => _validationErrors.AsEnumerable();

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public string MessageId { get; set; }

        /// <summary>
        /// Adds the specified validation errors to the result.
        /// </summary>
        /// <param name="validationErrors">The validation errors to add.</param>
        public void AddValidationErrors(IEnumerable<ValidationError> validationErrors)
        {
            _validationErrors.AddRange(validationErrors);
        }

        /// <summary>
        /// Marks this instance as complete.
        /// </summary>
        public void Complete()
        {
            this.Completed = DateTimeOffset.UtcNow;
        }
    }
}