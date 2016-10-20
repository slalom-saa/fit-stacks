using System;
using Autofac;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Logging;
using Slalom.FitStacks.Messaging.Validation;
using Slalom.FitStacks.Runtime;
using IComponentContext = Autofac.IComponentContext;

namespace Slalom.FitStacks.Messaging
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new CommandValidator(new ComponentContext(c.Resolve<IComponentContext>()))).As<ICommandValidator>();
            builder.Register(c => new CommandCoordinator(new ComponentContext(c.Resolve<IComponentContext>()), c.Resolve<IEventPublisher>(), c.Resolve<ICommandValidator>())).As<ICommandCoordinator>();
            builder.Register(c => new MessageBus(c.Resolve<IExecutionContextResolver>(), c.Resolve<ICommandCoordinator>())).As<IMessageBus>();
            builder.Register(c => new EventPublisher(new ComponentContext(c.Resolve<IComponentContext>()), c.Resolve<ILogger>())).As<IEventPublisher>();
        }
    }
}