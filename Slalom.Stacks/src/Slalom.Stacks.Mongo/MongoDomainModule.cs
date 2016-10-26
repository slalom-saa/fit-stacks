using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Mongo
{
    public class MongoDomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new MongoMappingsManager(c.Resolve<IDiscoverTypes>()))
                   .As<MongoMappingsManager>()
                   .SingleInstance();

            builder.Register(c => new ConfigurationBuilder().Build())
                   .As<IConfigurationRoot>()
                   .PreserveExistingDefaults();

            builder.Register(c => new MongoConnectionManager(c.Resolve<IConfigurationRoot>(), c.Resolve<MongoMappingsManager>()))
                   .As<MongoConnectionManager>()
                   .SingleInstance();
        }
    }
}