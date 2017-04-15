using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Modules;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// Contains methods to configure a <see cref="Stack"/>.
    /// </summary>
    public static class MessagingExtensions
    {
        public static IEnumerable<RequestEntry> GetRequests(this Stack instance)
        {
            return instance.Container.Resolve<IRequestLog>().GetEntries(null, null).Result;
        }

        public static IEnumerable<ResponseEntry> GetResponses(this Stack instance)
        {
            return instance.Container.Resolve<IResponseLog>().GetEntries(null, null).Result;
        }
    }
}