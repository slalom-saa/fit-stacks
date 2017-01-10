﻿using System;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Logging
{
    /// <summary>
    /// Represents an audit log entry, or information about an event that changed state.
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEntry"/> class.
        /// </summary>
        /// <param name="instance">The event.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        public AuditEntry(IEvent instance)
        {
            Argument.NotNull(instance, nameof(instance));

            try
            {
                this.Payload = JsonConvert.SerializeObject(instance, new JsonSerializerSettings
                {
                    ContractResolver = new EventContractResolver()
                });
            }
            catch
            {
                this.Payload = "{ \"Error\" : \"Serialization failed.\" }";
            }
            var context = instance.Context;
            this.EventName = instance.EventName;
            this.EventId = instance.Id;
            this.TimeStamp = instance.TimeStamp;
            this.MachineName = context.MachineName;
            this.Environment = context.Environment;
            this.ApplicationName = context.ApplicationName;
            this.SessionId = context.SessionId;
            this.UserName = context.User?.Identity?.Name;
            this.Path = context.Path;
            this.SourceAddress = context.SourceAddress;
            this.ThreadId = context.ThreadId;
            this.CorrelationId = context.CorrelationId;
            this.EventTypeId = instance.EventTypeId;
        }

        /// <summary>
        /// Gets the event type identifier used to classify the event.
        /// </summary>
        /// <value>The event type identifier used to classify the event.</value>
        public int EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>The event identifier.</value>
        public string EventId { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the path or URL.
        /// </summary>
        /// <value>The path or URL.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public string Payload { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTimeOffset? TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the user host address.
        /// </summary>
        /// <value>The user host address.</value>
        public string SourceAddress { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}