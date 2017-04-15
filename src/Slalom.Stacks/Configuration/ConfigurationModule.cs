using System;
using System.Collections.ObjectModel;
using System.IO;
using Autofac;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Caching;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Domain.Modules;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Modules;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Environment = Slalom.Stacks.Runtime.Environment;
using Module = Autofac.Module;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// An Autofac module that wires up root dependencies for the stack.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class ConfigurationModule : Module
    {
        private readonly Stack _stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationModule" /> class.
        /// </summary>
        /// <param name="stack">The current stack.</param>
        public ConfigurationModule(Stack stack)
        {
            _stack = stack;
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

            builder.RegisterModule(new DomainModule(_stack));
            builder.RegisterModule(new MessagingModule(_stack));
            builder.RegisterModule(new SearchModule(_stack));
            builder.RegisterModule(new RuntimeModule());
            builder.RegisterModule(new ReflectionModule(_stack));

            builder.RegisterModule(new LoggingModule());
            builder.RegisterModule(new NullCachingModule());

            builder.Register(c => new Environment(c.Resolve<IConfiguration>()))
                .As<IEnvironmentContext>();


            builder.Register(c => new DiscoveryService(c.Resolve<ILogger>()))
                   .As<IDiscoverTypes>()
                   .SingleInstance();
        }
    }
}