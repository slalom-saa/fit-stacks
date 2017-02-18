using System;
using System.IO;
using Autofac;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Caching;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Domain.Modules;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Modules;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Module = Autofac.Module;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// An Autofac module that wires up root dependencies for the stack.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class ConfigurationModule : Module
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationModule"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public ConfigurationModule(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

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

            builder.RegisterModule(new DomainModule(_assemblies));
            builder.RegisterModule(new MessagingModule(_assemblies));
            builder.RegisterModule(new SearchModule(_assemblies));
            builder.RegisterModule(new RuntimeModule());

            builder.RegisterModule(new LoggingModule());
            builder.RegisterModule(new NullCachingModule());

            builder.Register(c => new LocalExecutionContextResolver(c.Resolve<IConfiguration>()))
                   .As<IExecutionContextResolver>()
                   .SingleInstance();

            builder.Register(c => new DiscoveryService(c.Resolve<ILogger>()))
                   .As<IDiscoverTypes>()
                   .SingleInstance();
        }
    }
}