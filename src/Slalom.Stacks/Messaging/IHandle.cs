using System;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    public interface IHandle
    {
        Task<Object> HandleAsync(object instance);

        void SetContext(ExecutionContext context);
    }

    public interface IHandle<T> : IHandle
    {
    }
}