using System;
using System.Reflection;
using Autofac;
using System.Linq;
using Slalom.Stacks.Communication.Validation;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;
using IComponentContext = Autofac.IComponentContext;
using Module = Autofac.Module;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// An Autofac module to configure the communication depdencies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class CommunicationModule : Module
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

            builder.Register(c => new CommandValidator(new ComponentContext(c.Resolve<IComponentContext>())))
                   .As<ICommandValidator>().SingleInstance();
            builder.Register(c => new CommandCoordinator(new ComponentContext(c.Resolve<IComponentContext>()), c.Resolve<IEventPublisher>(), c.Resolve<ICommandValidator>()))
                   .As<ICommandCoordinator>().SingleInstance();
            builder.Register(c => new MessageBus(c.Resolve<IExecutionContextResolver>(), c.Resolve<ICommandCoordinator>()))
                   .As<IMessageBus>().SingleInstance();
            builder.Register(c => new EventPublisher(new ComponentContext(c.Resolve<IComponentContext>()), c.Resolve<ILogger>()))
                   .As<IEventPublisher>().SingleInstance();

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(ICommandHandler<,>)))
                   .As(instance =>
                   {
                       var interfaces = instance.GetInterfaces().Where(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
                       return interfaces.Select(e => typeof(ICommandHandler<,>).MakeGenericType(e.GetGenericArguments()[0], e.GetGenericArguments()[1]));
                   });

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IHandleEvent<>)))
                   .As(instance =>
                   {
                       var interfaces = instance.GetInterfaces().Where(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IHandleEvent<>));
                       return interfaces.Select(e => typeof(IHandleEvent<>).MakeGenericType(e.GetGenericArguments()[0]));
                   });

            builder.RegisterAssemblyTypes(this.Assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IValidationRule<,>)))
                   .As(instance =>
                   {
                       var interfaces = instance.GetInterfaces().Where(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IValidationRule<,>));
                       return interfaces.Select(e => typeof(IValidationRule<,>).MakeGenericType(e.GetGenericArguments()[0], e.GetGenericArguments()[1]));
                   });
        }
    }
}