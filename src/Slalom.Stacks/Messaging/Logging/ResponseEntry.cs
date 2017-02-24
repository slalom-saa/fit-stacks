using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Logging
{
    public class ResponseEntry
    {
        public ResponseEntry(MessageExecutionContext context)
        {
            this.CorrelationId = context.Request.CorrelationId;
            this.MessageId = context.Request.Message.Id;
            this.Completed = context.Completed;
            this.Response = context.Entry.Type.Name;
            this.Exception = context.Exception;
            this.IsSuccessful = context.IsSuccessful;
            this.Started = context.Started;
            this.ValidationErrors = context.ValidationErrors;
            this.MachineName = context.Execution.MachineName;
            this.ThreadId = context.Execution.ThreadId;
            this.ApplicationName = context.Execution.ApplicationName;
            this.Environment = context.Execution.Environment;
            if (this.Completed.HasValue)
            {
                this.Elapsed = this.Completed.Value - this.Started;
            }

            if (context.Response is IEvent)
            {
                var target = (IEvent) context.Response;
                this.EventName = target.EventName;
                this.EventId = target.Id;
                try
                {
                    this.EventPayload = JsonConvert.SerializeObject(target, new JsonSerializerSettings
                    {
                        ContractResolver = new EventContractResolver()
                    });
                }
                catch
                {
                    this.EventPayload = "{ \"Error\" : \"Serialization failed.\" }";
                }
            }
        }

        public string ApplicationName { get; set; }
        public DateTimeOffset? Completed { get; }
        public string CorrelationId { get; set; }
        public TimeSpan Elapsed { get; set; }

        public string Environment { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string EventPayload { get; set; }
        public Exception Exception { get; }
        public bool IsSuccessful { get; }

        public string MachineName { get; set; }
        public string MessageId { get; }

        public string Response { get; }
        public DateTimeOffset Started { get; }

        public int ThreadId { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; }
    }
}