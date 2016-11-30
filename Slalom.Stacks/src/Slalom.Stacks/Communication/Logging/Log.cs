using System;
using System.Linq;
using Newtonsoft.Json;
using Slalom.Stacks.Communication.Serialization;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Communication.Logging
{
    public class Log
    {
        public Log(ICommand command, ICommandResult result, ExecutionContext context)
        {
            try
            {
                this.Payload = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new JsonEventContractResolver()
                });
            }
            catch
            {
                this.Payload = "{ \"Error\" : \"Serialization failed.\" }";
            }
            this.IsSuccessful = result.IsSuccessful;
            this.CommandName = command.CommandName;
            this.CommandId = command.Id;
            this.TimeStamp = command.TimeStamp;
            if (result.ValidationErrors?.Any() ?? false)
            {
                this.ValidationErrors = JsonConvert.SerializeObject(result.ValidationErrors);
            }

            this.MachineName = context.MachineName;
            this.Environment = context.Environment;
            this.ApplicationName = context.ApplicationName;
            this.SessionId = context.SessionId;
            this.UserName = context.User?.Identity?.Name;
            this.Path = context.Path;
            this.UserHostAddress = context.UserHostAddress;
            this.Host = context.Host;
            this.ThreadId = context.ThreadId;
        }

        public string ApplicationName { get; set; }

        public Guid CommandId { get; set; }

        public string CommandName { get; set; }

        public Guid CorrelationId { get; set; }

        public string Environment { get; set; }

        public string Host { get; set; }

        public int Id { get; set; }

        public bool IsSuccessful { get; set; }

        public string MachineName { get; set; }

        public string Path { get; set; }

        public string Payload { get; set; }

        public string SessionId { get; set; }

        public int ThreadId { get; set; }

        public DateTimeOffset? TimeStamp { get; set; }

        public string UserHostAddress { get; set; }

        public string UserName { get; set; }

        public string ValidationErrors { get; set; }
    }
}