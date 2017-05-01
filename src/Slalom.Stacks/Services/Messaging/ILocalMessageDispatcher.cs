using System;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Inventory;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// Dispatches messages locally, or within process.
    /// </summary>
    public interface ILocalMessageDispatcher
    {
        /// <summary>
        /// Dispatches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="endPoint">The endpoint to dispatch to.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task<MessageResult> Dispatch(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null);
    }   
}