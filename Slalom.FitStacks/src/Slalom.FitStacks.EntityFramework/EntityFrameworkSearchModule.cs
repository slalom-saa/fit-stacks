using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.FitStacks.EntityFramework
{
    public class EntityFrameworkSearchModule : Module
    {
        public EntityFrameworkSearchModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
