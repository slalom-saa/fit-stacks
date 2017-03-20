using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IHandle<TMessage>
    {
        Task Handle(TMessage message);
    }

    public interface IEndPoint
    {
        ExecutionContext Context { get; set; }

        Request Request { get; }

    }

    public interface IEndPoint<TMessage> : IEndPoint
    {
       
        Task Receive(TMessage instance);
    }

    public interface IEndPoint<TRequest, TResponse> : IEndPoint
    {
        Task<TResponse> Receive(TRequest instance);
    }
}