using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Logging
{
    public class ActionEntry
    {
        public ActionEntry(MessageExecutionContext context)
        {
            this.CorrelationId = context.Request.CorrelationId;
            this.MessageId = context.Request.Message.Id;
            this.Completed = context.Completed;
            this.Action = context.Entry.Type.Name;
            this.Exception = context.Exception;
            this.IsSuccessful = context.IsSuccessful;
            this.Started = context.Started;
            this.ValidationErrors = context.ValidationErrors;
            if (this.Completed.HasValue)
            {
                this.Elapsed = this.Completed.Value - this.Started;
            }

            if (context.Response is IEvent)
            {
                this.Event = context.Response.GetType().Name;
                try
                {
                    this.EventPayload = JsonConvert.SerializeObject(context.Response, new JsonSerializerSettings
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

        public string Action { get; }
        public DateTimeOffset? Completed { get; }
        public string CorrelationId { get; set; }
        public TimeSpan Elapsed { get; set; }
        public Exception Exception { get; }
        public bool IsSuccessful { get; }
        public string MessageId { get; }
        public string Event { get; set; }

        public string EventPayload { get; set; }
        public DateTimeOffset Started { get; }

        public IEnumerable<ValidationError> ValidationErrors { get; }
    }
}