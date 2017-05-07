using System;
using System.Collections.Generic;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Utilities.NewId;
using Slalom.Stacks.Validation;
using Environment = Slalom.Stacks.Runtime.Environment;

namespace Slalom.Stacks.Services.Logging
{
    /// <summary>
    /// An entry to capture the response, or action, of a request.
    /// </summary>
    /// <remarks>
    /// The entry is intended to be created on the same process and thread as the executing
    /// endpoint.  It can then be passed and/or stored as needed.
    /// </remarks>
    public class ResponseEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEntry" /> class.
        /// </summary>
        public ResponseEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEntry" /> class.
        /// </summary>
        /// <param name="context">The completed context.</param>
        /// <param name="environment">The current environment.</param>
        public ResponseEntry(ExecutionContext context, Environment environment)
        {
            this.CorrelationId = context.Request.CorrelationId;
            this.RequestId = context.Request.Message.Id;
            this.Completed = context.Completed;
            this.EndPoint = context.EndPoint.ServiceType.AssemblyQualifiedName;
            this.Exception = context.Exception?.ToString();
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

        /// <summary>
        /// Gets or sets application name where the endpoint executed.
        /// </summary>
        /// <value>The application name where the endpoint executed.</value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets the execution completion date and time.
        /// </summary>
        /// <value>The execution completion date and time.</value>
        public DateTimeOffset? Completed { get; set; }

        /// <summary>
        /// Gets or sets the request correlation identifier.
        /// </summary>
        /// <value>The request correlation identifier.</value>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the execution time elapsed from start to end.
        /// </summary>
        /// <value>The execution time elapsed from start to end.</value>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// Gets the type of the endpoint.
        /// </summary>
        /// <value>The type of the endpoint.</value>
        public string EndPoint { get; set; }

        /// <summary>
        /// Gets or sets the name of the environment.  This should be DEV, QA, PROD, etc.
        /// </summary>
        /// <value>The name of the environment.</value>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets the exception that was raised, if any.
        /// </summary>
        /// <value>The exception that was raised, if any.</value>
        public string Exception { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; } = NewId.NextId();

        /// <summary>
        /// Gets a value indicating whether execution was successful.
        /// </summary>
        /// <value><c>true</c> if execution was; otherwise, <c>false</c>.</value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine that executed the endpoint.
        /// </summary>
        /// <value>The name of the machine that executed the endpoint.</value>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the path that the endpoint was listening on, if any.
        /// </summary>
        /// <value>The path that the endpoint was listening on.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets the request message identifier.
        /// </summary>
        /// <value>The request message identifier.</value>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets the date and time that the request was received.
        /// </summary>
        /// <value>The date and time that the request was received.</value>
        public DateTimeOffset Started { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the thread that executed the endpoint.
        /// </summary>
        /// <value>The identifier of the thread that executed the endpoint.</value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this entry was created.
        /// </summary>
        /// <value>The date and time when this entry was created.</value>
        public DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the validation errors that were raised, if any.
        /// </summary>
        /// <value>The validation errors that were raised.</value>
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}