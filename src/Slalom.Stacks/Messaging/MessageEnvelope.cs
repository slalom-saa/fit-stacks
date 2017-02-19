using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    public class MessageEnvelope
    {
        public IMessage Message { get; }
        public ExecutionContext Context { get; }

        public MessageEnvelope(IMessage message, ExecutionContext context)
        {
            this.Message = message;
            this.Context = context;
        }
    }
}
