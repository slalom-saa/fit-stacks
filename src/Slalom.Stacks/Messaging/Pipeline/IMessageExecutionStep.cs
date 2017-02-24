using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public interface IMessageExecutionStep
    {
        Task Execute(IMessage message, MessageExecutionContext context);
    }
}
