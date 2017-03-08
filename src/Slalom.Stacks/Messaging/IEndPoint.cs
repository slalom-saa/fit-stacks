using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IHandle<TMessage>
    {
        Task Handle(TMessage message);
    }

    public interface IEndPoint<TMessage>
    {
        Task Receive(TMessage instance);
    }

    public interface IEndPoint<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest instance);
    }
}