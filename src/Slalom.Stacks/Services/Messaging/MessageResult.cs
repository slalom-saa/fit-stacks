using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// The result of message execution.  Contains information about the execution and the response from
    /// the actor.
    /// </summary>
    public class MessageResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageResult" /> class.
        /// </summary>
        /// <param name="context">The completed context.</param>
        public MessageResult(ExecutionContext context)
        {
            this.CorrelationId = context.Request.CorrelationId;
            this.Started = DateTimeOffset.UtcNow;
            this.Completed = context.Completed;
            this.RaisedException = context.Exception;
            this.Response = context.Response;
            this.ValidationErrors = context.ValidationErrors.ToList();
            this.RequestId = context.Request.Message.Id;
            this.IsCancelled = context.CancellationToken.IsCancellationRequested;
        }

        /// <summary>
        /// Gets the date and time completed.
        /// </summary>
        /// <value>The date and time completed.</value>
        public DateTimeOffset? Completed { get; }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; }

        /// <summary>
        /// Gets the time elapsed.
        /// </summary>
        /// <value>The time elapsed.</value>
        public TimeSpan? Elapsed
        {
            get
            {
                var timeSpan = this.Completed - this.Started;
                if (timeSpan != null)
                {
                    return new TimeSpan(Math.Max(timeSpan.Value.Ticks, 0));
                }
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this message execution was cancelled.
        /// </summary>
        /// <value><c>true</c> if this message execution was cancelled; otherwise, <c>false</c>.</value>
        public bool IsCancelled { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the execution was successful.
        /// </summary>
        /// <value><c>true</c> if the execution was successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful => !this.ValidationErrors.Any() && this.RaisedException == null;

        /// <summary>
        /// Gets the raised exception if any.
        /// </summary>
        /// <value>The raised exception.</value>
        public Exception RaisedException { get; }

        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        /// <value>The request ID.</value>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets the actor response.
        /// </summary>
        public object Response { get; set; }

        /// <summary>
        /// Gets or sets the date and time started.
        /// </summary>
        /// <value>The date and time started.</value>
        public DateTimeOffset Started { get; }

        /// <summary>
        /// Gets any validation errors that were raised.
        /// </summary>
        /// <value>The validation errors that were raised.</value>
        public IReadOnlyList<ValidationError> ValidationErrors { get; }
    }
}