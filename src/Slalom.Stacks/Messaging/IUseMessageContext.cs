using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IUseMessageContext
    {
        void SetContext(MessageContext context);

        MessageContext Context { get; }
    }
}
