using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Logging
{
    /// <summary>
    /// An entry to capture the response, or action, of a request.
    /// </summary>
    public class ResponseEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEntry"/> class.
        /// </summary>
        /// <param name="context">The completed context.</param>
        public ResponseEntry(MessageExecutionContext context)
        {
            this.CorrelationId = context.Request.CorrelationId;
            //this.MessageId = context.Request.Message.Id;
            this.Completed = context.Completed;
            this.Service = context.EndPoint.EndPointType;
            this.Exception = context.Exception;
            this.IsSuccessful = context.IsSuccessful;
            this.Started = context.Started;
            this.ValidationErrors = context.ValidationErrors;
            this.MachineName = context.ExecutionContext.MachineName;
            this.ThreadId = context.ExecutionContext.ThreadId;
            this.ApplicationName = context.ExecutionContext.ApplicationName;
            this.Environment = context.ExecutionContext.Environment;
            if (this.Completed.HasValue)
            {
                this.Elapsed = this.Completed.Value - this.Started;
            }

            if (context.Response is IEvent)
            {
                var target = (IEvent)context.Response;
                this.EventType = target.GetType().FullName;
                this.EventId = target.Id;
                try
                {
                    this.EventBody = JsonConvert.SerializeObject(target, new JsonSerializerSettings
                    {
                        ContractResolver = new EventContractResolver()
                    });
                }
                catch
                {
                    this.EventBody = "{ \"Error\" : \"Serialization failed.\" }";
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
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

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the event body.
        /// </summary>
        /// <value>The event body.</value>
        public string EventBody { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>The event identifier.</value>
        public string EventId { get; set; }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType { get; set; }

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

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
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
        public string Service { get; }

        /// <summary>
        /// Gets the start date and time.
        /// </summary>
        /// <value>The start date and time.</value>
        public DateTimeOffset Started { get; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the validation errors that were raised, if any.
        /// </summary>
        /// <value>The validation errors that were raised, if any.</value>
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}