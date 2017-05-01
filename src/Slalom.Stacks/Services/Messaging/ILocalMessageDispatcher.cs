using System;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Inventory;

namespace Slalom.Stacks.Services.Messaging
{
    public interface IRemoteMessageDispatcher
    {
        bool CanDispatch(Request request);

        Task<MessageResult> Dispatch(Request request, ExecutionContext parentContext, TimeSpan? timeout = null);
    }

    public interface ILocalMessageDispatcher
    {
        Task<MessageResult> Dispatch(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null);
    }   
}