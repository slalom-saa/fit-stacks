using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Communication.Logging;

namespace Slalom.Stacks.EntityFramework
{
    public class LoggingModule<TContext> : Module where TContext : DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<IAuditStore>(c => new AuditStore(c.Resolve<TContext>()));
            builder.Register<ILogStore>(c => new LogStore(c.Resolve<TContext>()));
        }
    }
}
