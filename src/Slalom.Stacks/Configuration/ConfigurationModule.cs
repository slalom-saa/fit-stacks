using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Caching;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;
using Module = Autofac.Module;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// An Autofac module that wires up root dependencies for the stack.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class ConfigurationModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationModule"/> class.
        /// </summary>
        /// <param name="indicators">Any indicators as types or instances for assembly scanning.</param>
        public ConfigurationModule(params object[] indicators)
        {
            var target = new List<Assembly>();
            foreach (var instance in indicators)
            {
                var type = instance as Type;
                if (type != null)
                {
                    target.Add(type.GetTypeInfo().Assembly);
                }
                else
                {
                    target.Add(instance.GetType().GetTypeInfo().Assembly);
                }
            }
            this.Assemblies = target.ToArray();
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

            builder.Register(c =>
                   {
                       var configurationBuilder = new ConfigurationBuilder();
                       configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                       configurationBuilder.AddJsonFile("appsettings.json", true, true);
                       return configurationBuilder.Build();
                   }).As<IConfiguration>()
                   .SingleInstance();

            builder.Register<ILogger>(c => new NullLogger())
                .SingleInstance();

            builder.RegisterModule(new DomainModule(this.Assemblies));
            builder.RegisterModule(new CommunicationModule(this.Assemblies));
            builder.RegisterModule(new SearchModule(this.Assemblies));
            builder.RegisterModule(new CachingModule());

            builder.Register(c => new ComponentContext(c.Resolve<Autofac.IComponentContext>()))
                   .As<IComponentContext>();

#if !NET461
            builder.Register(c => new LocalExecutionContextResolver(c.Resolve<IConfiguration>()))
                .As<IExecutionContextResolver>()
                .SingleInstance();
#else
            builder.Register(c => new LocalExecutionContextResolver(c.Resolve<IConfiguration>()))
                .As<IExecutionContextResolver>()
                .SingleInstance();
#endif


            builder.Register(c => new DiscoveryService(c.Resolve<ILogger>()))
                   .As<IDiscoverTypes>()
                   .SingleInstance();
        }
    }
}