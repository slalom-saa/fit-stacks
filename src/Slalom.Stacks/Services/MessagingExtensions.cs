using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Services.Modules;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// Contains methods to configure a <see cref="Stack"/>.
    /// </summary>
    public static class MessagingExtensions
    {
        public static IEnumerable<RequestEntry> GetRequests(this Stack instance, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            return instance.Container.Resolve<IRequestLog>().GetEntries(start, end).Result;
        }

        public static IEnumerable<ResponseEntry> GetResponses(this Stack instance, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            return instance.Container.Resolve<IResponseLog>().GetEntries(start, end).Result;
        }

        public static IEnumerable<EventMessage> GetEvents(this Stack instance, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            return instance.Container.Resolve<IEventStore>().GetEvents(start, end).Result;
        }
    }
}