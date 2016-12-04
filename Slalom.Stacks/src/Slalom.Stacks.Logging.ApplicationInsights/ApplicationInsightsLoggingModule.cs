using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Communication.Logging;

namespace Slalom.Stacks.Logging.ApplicationInsights
{
    public class ApplicationInsightsLoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<ILogStore>(c => new LogStore(c.Resolve<IConfiguration>()));
            builder.Register<IAuditStore>(c => new AuditStore(c.Resolve<IConfiguration>()));
        }
    }
}
