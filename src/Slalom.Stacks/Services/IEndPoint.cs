using System;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services
{
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