using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.FitStacks.Reflection;

namespace Slalom.FitStacks.DocumentDb
{
    public class DocumentDbDomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new DocumentDbMappingsManager(c.Resolve<IDiscoverTypes>()))
                   .As<DocumentDbMappingsManager>()
                   .SingleInstance();

            builder.Register(c => new ConfigurationBuilder().Build())
                   .As<IConfigurationRoot>()
                   .PreserveExistingDefaults();

            builder.Register(c => new DocumentDbConnectionManager(c.Resolve<IConfigurationRoot>(), c.Resolve<DocumentDbMappingsManager>(), c.Resolve<DocumentDbOptions>()))
                   .As<DocumentDbConnectionManager>()
                   .SingleInstance();
        }
    }
}