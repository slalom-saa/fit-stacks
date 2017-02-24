using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public class Complete : IMessageExecutionStep
    {
        public Task Execute(IMessage message, MessageExecutionContext context)
        {
            context.Complete();

            return Task.FromResult(0);
        }
    }
}
