using System;
using Autofac;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Validation;
using Module = Autofac.Module;

namespace Slalom.Stacks.Messaging.Modules
{
    /// <summary>
    /// An Autoface module that configures messaging items in other assemblies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class MessagingTypesModule : Module
    {
        private readonly Assembly[] _assemblies;

        public MessagingTypesModule(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IValidate<>)))
                   .As(instance => instance.GetBaseAndContractTypes())
                   .AllPropertiesAutowired();

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IHandle)))
                   .As(instance => instance.GetBaseAndContractTypes())
                   .AsSelf()
                   .AllPropertiesAutowired();
        }
    }
}