using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IHandle
    {
        Task<Object> HandleAsync(object instance);
    }

    public interface IHandle<T> : IHandle
    {
    }
}