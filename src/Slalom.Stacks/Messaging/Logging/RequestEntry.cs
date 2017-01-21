using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Utilities.NewId;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Logging
{
    /// <summary>
    /// Represents a request log entry - something that tracks the request at the application level.
    /// </summary>
    public class RequestEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEntry" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="result">The result.</param>
        /// <param name="context">The context.</param>
        public RequestEntry(ICommand command, CommandResult result, ExecutionContext context)
        {
            try
            {
                this.Payload = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new CommandContractResolver()
                });
            }
            catch
            {
                this.Payload = "{ \"Error\" : \"Serialization failed.\" }";
            }
            this.IsSuccessful = result.IsSuccessful;
            this.RequestName = command.CommandName;
            this.RequestId = command.Id;
            this.TimeStamp = command.TimeStamp;
            this.ValidationErrors = result.ValidationErrors?.ToArray();
            this.MachineName = context.MachineName;
            this.Environment = context.Environment;
            this.ApplicationName = context.ApplicationName;
            this.SessionId = context.SessionId;
            this.UserName = context.User?.Identity?.Name;
            this.Path = context.Path;
            this.SourceAddress = context.SourceAddress;
            this.ThreadId = context.ThreadId;
            this.CorrelationId = context.CorrelationId;
            this.RaisedException = result.RaisedException;
            this.Elapsed = result.Elapsed;
            this.Started = result.Started;
            this.Completed = result.Completed;
        }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the completed date and time.
        /// </summary>
        /// <value>The completed date and time.</value>
        public DateTimeOffset? Completed { get; set; }

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the elapsed time.
        /// </summary>
        /// <value>The elapsed time.</value>
        public TimeSpan? Elapsed { get; set; }

        /// <summary>
        /// Gets or sets the environment name.
        /// </summary>
        /// <value>The environment name.</value>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the instance identifier.
        /// </summary>
        /// <value>The instance identifier.</value>
        public string Id { get; set; } = NewId.NextId();

        /// <summary>
        /// Gets or sets a value indicating whether the execution was successful.
        /// </summary>
        /// <value><c>true</c> if this the execution was successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the request path or URL.
        /// </summary>
        /// <value>The request path or URL.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the request payload.
        /// </summary>
        /// <value>The request payload.</value>
        public string Payload { get; set; }

        /// <summary>
        /// Gets or sets the raised exception.
        /// </summary>
        /// <value>The raised exception.</value>
        public Exception RaisedException { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>The request identifier.</value>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the name of the request.
        /// </summary>
        /// <value>The name of the request.</value>
        public string RequestName { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the user host address.
        /// </summary>
        /// <value>The user host address.</value>
        public string SourceAddress { get; set; }

        /// <summary>
        /// Gets or sets the started date and time.
        /// </summary>
        /// <value>The started date and time.</value>
        public DateTimeOffset Started { get; set; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the message time stamp.
        /// </summary>
        /// <value>The the message stamp.</value>
        public DateTimeOffset? TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}