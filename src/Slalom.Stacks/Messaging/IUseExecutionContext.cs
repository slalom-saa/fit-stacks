using System;
using System.Linq;

namespace Slalom.Stacks.Messaging
{
    public interface IUseExecutionContext
    {
        void UseContext(ExecutionContext context);
    }
}