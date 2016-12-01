using System;
using Newtonsoft.Json;
using Slalom.Stacks.Communication.Serialization;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Communication.Logging
{
    public class Audit
    {
        public Audit(IEvent @event, ExecutionContext context)
        {
            try
            {
                this.Payload = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
                {
                    ContractResolver = new JsonEventContractResolver()
                });
            }
            catch
            {
                this.Payload = "{ \"Error\" : \"Serialization failed.\" }";
            }
            this.EventName = @event.EventName;
            this.EventId = @event.Id;
            this.TimeStamp = @event.TimeStamp;

            this.MachineName = context.MachineName;
            this.Environment = context.Environment;
            this.ApplicationName = context.ApplicationName;
            this.SessionId = context.SessionId;
            this.UserName = context.User?.Identity?.Name;
            this.Path = context.Path;
            this.UserHostAddress = context.UserHostAddress;
            this.Host = context.Host;
            this.ThreadId = context.ThreadId;
            this.CorrelationId = context.CorrelationId;
        }

        public string ApplicationName { get; set; }

        public string CorrelationId { get; set; }

        public string Environment { get; set; }

        public Guid? EventId { get; set; }

        public string EventName { get; set; }

        public string Host { get; set; }

        public int Id { get; set; }

        public string MachineName { get; set; }

        public string Path { get; set; }

        public string Payload { get; set; }

        public string SessionId { get; set; }

        public int ThreadId { get; set; }

        public DateTimeOffset? TimeStamp { get; set; }

        public string UserHostAddress { get; set; }

        public string UserName { get; set; }
    }
}