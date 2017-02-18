using System.Linq;
using System.Reflection;
using Autofac;
using Slalom.Stacks.Configuration;
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

            builder.RegisterAssemblyTypes(this._assemblies)
                  .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IHandleEvent<>)))
                  .AsImplementedInterfaces()
                  .SingleInstance();

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IHandleEvent)))
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IValidationRule<,>)))
                   .As(instance => instance.GetBaseAndContractTypes())
                   .PropertiesAutowired(AllProperties.Instance);

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IHandle<>)))
                   .As(instance => instance.GetBaseAndContractTypes())
                   .AsSelf()
                   .PropertiesAutowired(AllProperties.Instance);
        }
    }
}