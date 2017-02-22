using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Pipeline.Steps
{
    public class Complete : IMessageExecutionStep
    {
        public Task Execute(IMessage message, MessageContext context)
        {
            context.Complete();

            return Task.FromResult(0);
        }
    }
}
