using System;
using System.Linq;
using System.Reflection;
using Autofac;
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
    public class CommunicationModule : Module
    {
        public CommunicationModule(Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
        }

        public Assembly[] Assemblies { get; set; }

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