using System;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging
{
    public interface IRemoteMessageDispatcher
    {
        bool CanDispatch(Request request);

        Task<MessageResult> Dispatch(Request request, ExecutionContext parentContext, TimeSpan? timeout = null);
    }

    public interface ILocalMessageDispatcher
    {
        Task<MessageResult> Dispatch(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null);

        Task<MessageResult> Dispatch(Request request, ExecutionContext context);
    }   
}