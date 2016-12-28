using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Validation;
using IComponentContext = Slalom.Stacks.Configuration.IComponentContext;
using Module = Autofac.Module;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An Autofac module to configure the communication dependencies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class CommunicationModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationModule"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies used to probe.</param>
        public CommunicationModule(Assembly[] assemblies)
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

            builder.Register(c => new UseCaseCoordinator(c.Resolve<IComponentContext>()))
                   .As<IUseCaseCoordinator>();

            builder.Register(c => new EventPublisher(c.Resolve<IComponentContext>()))
                   .As<IEventPublisher>();

            builder.RegisterGeneric(typeof(CommandValidator<>));

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IHandleEvent<>)))
                   .As(instance =>
                   {
                       var interfaces = instance.GetInterfaces().Where(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IHandleEvent<>));
                       return interfaces.Select(e => typeof(IHandleEvent<>).MakeGenericType(e.GetGenericArguments()[0]));
                   });

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IValidationRule<,>)))
                   .As(instance => instance.GetBaseAndContractTypes());

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(UseCaseActor<,>)))
                   .As(instance => instance.GetBaseAndContractTypes())
                   .AsSelf();

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => typeof(IAuditStore).IsAssignableFrom(e))
                   .As<IAuditStore>();

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => typeof(IAuditStore).IsAssignableFrom(e))
                   .As<ILogStore>();
        }
    }
}