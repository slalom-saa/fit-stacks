using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Slalom.Stacks.Caching
{
    public class CachingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new PassthroughCacheManager())
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}
