using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Reflection;
using IComponentContext = Autofac.IComponentContext;
using Module = Autofac.Module;

namespace Slalom.FitStacks.Domain
{
    public class DomainModule : Module
    {
        public DomainModule(params Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
        }

        /// <summary>
        /// Gets or sets the assemblies used for discovery.
        /// </summary>
        /// <value>The assemblies used for discovery.</value>
        public Assembly[] Assemblies { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new DomainFacade(new ComponentContext(c.Resolve<IComponentContext>())))
                   .As<IDomainFacade>();

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IRepository<>)))
                   .As(e => e.GetBaseAndContractTypes().Where(x => !x.GetTypeInfo().IsGenericTypeDefinition));
        }
    }
}