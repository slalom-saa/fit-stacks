using System;
using System.Collections.Generic;
using System.Reflection;
using Slalom.Stacks.Validation;
using Environment = Slalom.Stacks.Runtime.Environment;

namespace Slalom.Stacks.Messaging.Logging
{
    /// <summary>
    /// An entry to capture the response, or action, of a request.
    /// </summary>
    public class ResponseEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEntry" /> class.
        /// </summary>
        /// <param name="context">The completed context.</param>
        /// <param name="environment">The environment.</param>
        public ResponseEntry(ExecutionContext context, Environment environment)
        {
            this.CorrelationId = context.Request.CorrelationId;
            this.MessageId = context.Request.Message.Id;
            this.Completed = context.Completed;
            this.Service = context.EndPoint.ServiceType;
            this.Exception = context.Exception;
            this.IsSuccessful = context.IsSuccessful;
            this.Started = context.Started;
            this.ValidationErrors = context.ValidationErrors;
            this.TimeStamp = DateTimeOffset.Now;
            this.MachineName = environment.MachineName;
            this.ApplicationName = environment.ApplicationName;
            this.EnvironmentName = environment.EnvironmentName;
            this.ThreadId = environment.ThreadId;
            this.Path = context.Request.Path;
            if (this.Completed.HasValue)
            {
                this.Elapsed = this.Completed.Value - this.Started;
            }
        }

        public string Path { get; set; }

        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets the completion date and time.
        /// </summary>
        /// <value>The completion date and time.</value>
        public DateTimeOffset? Completed { get; }

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the time elapsed from start to end.
        /// </summary>
        /// <value>The time elapsed from start to end.</value>
        public TimeSpan Elapsed { get; set; }

        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets the exception, if any.
        /// </summary>
        /// <value>The exception, if any.</value>
        public Exception Exception { get; }

        /// <summary>
        /// Gets a value indicating whether execution was successful.
        /// </summary>
        /// <value><c>true</c> if execution was; otherwise, <c>false</c>.</value>
        public bool IsSuccessful { get; }

        public string MachineName { get; set; }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public string MessageId { get; }

        /// <summary>
        /// Gets the type of the endPoint.
        /// </summary>
        /// <value>The type of the endPoint.</value>
        public Type Service { get; }

        /// <summary>
        /// Gets the start date and time.
        /// </summary>
        /// <value>The start date and time.</value>
        public DateTimeOffset Started { get; }

        public int ThreadId { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the validation errors that were raised, if any.
        /// </summary>
        /// <value>The validation errors that were raised, if any.</value>
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}