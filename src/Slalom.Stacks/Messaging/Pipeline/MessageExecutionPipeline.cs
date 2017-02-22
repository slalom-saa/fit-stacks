using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Pipeline.Steps;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public class MessageExecutionPipeline : IMessageExecutionPipeline
    {
        private readonly IComponentContext _components;
        private readonly Lazy<List<IMessageExecutionStep>> _steps = new Lazy<List<IMessageExecutionStep>>();

        public MessageExecutionPipeline(IComponentContext components)
        {
            Argument.NotNull(components, nameof(components));

            _components = components;

            _steps = new Lazy<List<IMessageExecutionStep>>(() => new List<IMessageExecutionStep>
            {
                components.Resolve<LogStart>(),
                components.Resolve<ValidateMessage>(),
                components.Resolve<ExecuteHandler>(),
                components.Resolve<HandleException>(),
                components.Resolve<Complete>(),
                components.Resolve<PublishEvents>(),
                components.Resolve<LogCompletion>()
            });
        }

        public async Task Execute(IMessage message, MessageContext context)
        {
            foreach (var step in _steps.Value)
            {
                await step.Execute(message, context);
            }
        }
    }

    public interface IMessageExecutionPipeline
    {
        Task Execute(IMessage message, MessageContext context);
    }
}
