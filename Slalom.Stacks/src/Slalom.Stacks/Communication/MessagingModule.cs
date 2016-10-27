using System;
using Autofac;
using Slalom.Stacks.Communication.Validation;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Runtime;
using IComponentContext = Autofac.IComponentContext;

namespace Slalom.Stacks.Communication
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new CommandValidator(new ComponentContext(c.Resolve<IComponentContext>()))).As<ICommandValidator>().SingleInstance();
            builder.Register(c => new CommandCoordinator(new ComponentContext(c.Resolve<IComponentContext>()), c.Resolve<IEventPublisher>(), c.Resolve<ICommandValidator>())).As<ICommandCoordinator>().SingleInstance();
            builder.Register(c => new MessageBus(c.Resolve<IExecutionContextResolver>(), c.Resolve<ICommandCoordinator>())).As<IMessageBus>().SingleInstance();
            builder.Register(c => new EventPublisher(new ComponentContext(c.Resolve<IComponentContext>()), c.Resolve<ILogger>())).As<IEventPublisher>().SingleInstance();
        }
    }
}