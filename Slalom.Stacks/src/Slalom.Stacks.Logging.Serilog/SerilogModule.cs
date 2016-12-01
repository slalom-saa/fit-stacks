using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Logging.Serilog
{
    public class SerilogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new DestructuringPolicy()).As<IDestructuringPolicy>();
            builder.Register(c => new SerilogLogger(c.Resolve<IConfiguration>(), c.Resolve<IExecutionContextResolver>(), c.Resolve<IEnumerable<IDestructuringPolicy>>())).As<ILogger>();
        }
    }
}