using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Reflection;
using IComponentContext = Autofac.IComponentContext;
using Module = Autofac.Module;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Autofac module that registers search dependencies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class SearchModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchModule"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies to probe for needed components.</param>
        public SearchModule(Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
        }

        /// <summary>
        /// Gets or sets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
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

            builder.Register(c => new InMemorySearchContext())
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance();

            builder.Register(c => new SearchFacade(new ComponentContext(c.Resolve<IComponentContext>())))
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterGeneric(typeof(SearchIndexer<>))
                   .As(typeof(ISearchIndexer<>))
                   .InstancePerDependency();

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(ISearchIndexer<>)))
                   .As(instance =>
                   {
                       var interfaces = instance.GetInterfaces().Where(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(ISearchIndexer<>));
                       return interfaces.Select(e => typeof(ISearchIndexer<>).MakeGenericType(e.GetGenericArguments()[0]));
                   }).InstancePerDependency();
        }
    }
}