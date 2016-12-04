using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Communication.Logging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Logging.ApplicationInsights
{
    public class AuditStore : IAuditStore
    {
        private readonly TelemetryClient _client;

        public AuditStore(IConfiguration configuration)
        {
            _client = new TelemetryClient();
            _client.InstrumentationKey = configuration["ApplicationInsights:InstrumentationKey"];
        }

        public Task AppendAsync(IEvent @event, ExecutionContext context)
        {
            if (!(@event is CommandExecutionFailedEvent))
            {
                var instance = new EventTelemetry(@event.EventName);
                instance.Context.User.Id = "me";
                instance.Context.Session.Id = context.SessionId;

                _client.TrackEvent(instance);

                _client.Flush();
            }

            return Task.FromResult(0);
        }
    }
}
