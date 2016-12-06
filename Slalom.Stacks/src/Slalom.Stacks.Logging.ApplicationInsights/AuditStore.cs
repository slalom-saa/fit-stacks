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
    public sealed class AuditStore : IAuditStore, IDisposable
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
                instance.Context.User.Id = context.User?.Identity?.Name;
                instance.Context.Session.Id = context.SessionId;

                _client.TrackEvent(instance);
            }

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _client.Flush();
        }
    }
}
