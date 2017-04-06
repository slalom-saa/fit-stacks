using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Logging;

namespace Slalom.Stacks.Reflection
{
    public class ReflectionModule : Module
    {
        private readonly Stack _stack;

        public ReflectionModule(Stack stack)
        {
            _stack = stack;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new DiscoveryService(c.Resolve<ILogger>())).AsSelf().AsImplementedInterfaces();
        }
    }
}
