using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Reflection;
using IComponentContext = Autofac.IComponentContext;
using Module = Autofac.Module;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// An Autofac module that wires up dependencies for the domain module.
    /// </summary>
    public class DomainModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainModule"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public DomainModule(params Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
        }

        /// <summary>
        /// Gets or sets the assemblies used for discovery.
        /// </summary>
        /// <value>The assemblies used for discovery.</value>
        public Assembly[] Assemblies { get; set; }

        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new DomainFacade(new ComponentContext(c.Resolve<IComponentContext>())))
                   .As<IDomainFacade>()
                   .SingleInstance();

            builder.Register(e => new InMemoryEntityContext())
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterGeneric(typeof(Repository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerDependency();
        }
    }
}