using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Autofac;
using Slalom.Stacks.Messaging.Persistence;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Validation;
using Module = Autofac.Module;

namespace Slalom.Stacks.Messaging.Modules
{
    /// <summary>
    /// An Autofac module to configure the communication dependencies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class MessagingModule : Module
    {
        private readonly Stack _stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingModule"/> class.
        /// </summary>
        public MessagingModule(Stack stack)
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

            builder.Register(c => new MessageGateway(c.Resolve<IComponentContext>()))
                   .As<IMessageGateway>()
                   .SingleInstance();

            builder.RegisterType<LocalMessageDispatcher>().As<IMessageDispatcher>();

            builder.RegisterAssemblyTypes(_stack.Assemblies.Union(new[] { typeof(IMessageExecutionStep).GetTypeInfo().Assembly }).ToArray())
                .Where(e => e.GetInterfaces().Any(x => x == typeof(IMessageExecutionStep)))
                .AsSelf();

            builder.Register(c => new ServiceRegistry())
                .AsSelf()
                .SingleInstance()
                .OnActivated(e =>
                   {
                       e.Instance.RegisterLocal(_stack.Assemblies.ToArray());
                   });

            builder.Register(c => new Request())
                .As<IRequestContext>();

            builder.RegisterType<NullRequestLog>().As<IRequestLog>().SingleInstance();
            builder.RegisterType<NullResponseLog>().As<IResponseLog>().SingleInstance();

            builder.RegisterType<NullEventStore>().As<IEventStore>().SingleInstance();

            builder.RegisterGeneric(typeof(MessageValidator<>));

            builder.RegisterAssemblyTypes(_stack.Assemblies.ToArray())
                  .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IValidate<>)))
                  .As(instance => instance.GetBaseAndContractTypes())
                  .AllPropertiesAutowired();

            builder.RegisterAssemblyTypes(_stack.Assemblies.ToArray())
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IEndPoint<>) || x == typeof(IHandle<>)))
                   .As(instance => instance.GetBaseAndContractTypes())
                   .AsSelf()
                   .AllPropertiesAutowired();

            _stack.Assemblies.CollectionChanged += (sender, args) =>
            {
                _stack.Use(b =>
                {
                    b.RegisterAssemblyTypes(args.NewItems.OfType<Assembly>().ToArray())
                        .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IValidate<>)))
                        .As(instance => instance.GetBaseAndContractTypes())
                        .AllPropertiesAutowired();

                    b.RegisterAssemblyTypes(args.NewItems.OfType<Assembly>().ToArray())
                           .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(IEndPoint<>) || x == typeof(IHandle<>)))
                           .As(instance => instance.GetBaseAndContractTypes())
                           .AsSelf()
                           .AllPropertiesAutowired();
                });
            };
        }
    }
}