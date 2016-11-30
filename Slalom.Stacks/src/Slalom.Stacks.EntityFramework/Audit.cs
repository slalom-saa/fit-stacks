using System;
using System.Reflection;
using Newtonsoft.Json;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Communication.Serialization;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.EntityFramework
{
    public class Audit
    {
        public Audit(ICommand command, ICommandResult result, ExecutionContext context)
        {
            var @event = ((dynamic)result).Value as Event;

            this.CommandId = command.Id;
            if (@event != null)
            {
                try
                {
                    this.CommandPayload = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonCommandContractResolver()
                    });
                }
                catch
                {
                    this.EventPayload = "{ \"Error\" : \"Serialization failed.\" }";
                }
            }
            this.CommandName = command.CommandName;
            this.CommandTimeStamp = command.TimeStamp;

            try
            {
                this.EventPayload = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
                {
                    ContractResolver = new JsonEventContractResolver()
                });
            }
            catch
            {
                this.EventPayload = "{ \"Error\" : \"Serialization failed.\" }";
            }
            this.EventName = @event?.EventName;
            this.EventId = @event?.Id;
            this.EventTimeStamp = @event?.TimeStamp;

            this.MachineName = context.MachineName;
            this.Environment = context.Environment;
            this.ApplicationName = context.ApplicationName;
            this.SessionId = context.SessionId;
            this.UserName = context.User?.Identity?.Name;

            this.IsSuccessful = result.IsSuccessful;
            this.ChangesState = typeof(Event).IsAssignableFrom(command.GetType().GetTypeInfo().BaseType?.GetGenericArguments()[0]);
        }

        public bool ChangesState { get; set; }

        public string Environment { get; set; }

        public string CommandName { get; set; }

        public Guid CommandId { get; set; }

        public string CommandPayload { get; set; }

        public string MachineName { get; set; }

        public string ApplicationName { get; set; }

        public Guid CorrelationId { get; set; }

        public string SessionId { get; set; }

        public string EventPayload { get; set; }

        public string EventName { get; set; }

        public Guid? EventId { get; set; }

        public string UserName { get; set; }

        public DateTimeOffset? EventTimeStamp { get; set; }

        public DateTimeOffset CommandTimeStamp { get; set; }

        public bool IsSuccessful { get; set; }

        public int Id { get; set; }
    }
}