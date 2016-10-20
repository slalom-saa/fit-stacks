﻿using System;
using Autofac;
using Destructurama.Attributed;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Slalom.FitStacks.Runtime;
using System.Collections.Generic;

namespace Slalom.FitStacks.Logging
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new LoggingDestructuringPolicy()).As<IDestructuringPolicy>();
            builder.Register(c => new SerilogLogger(c.Resolve<IConfiguration>(), c.Resolve<IExecutionContextResolver>(), c.Resolve<IEnumerable<IDestructuringPolicy>>())).As<ILogger>();
        }
    }
}