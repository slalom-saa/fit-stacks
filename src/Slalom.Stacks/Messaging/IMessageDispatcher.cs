using System;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Dispatches messages to a specific channel or channels.
    /// </summary>
    public interface IMessageDispatcher
    {
        /// <summary>
        /// Determines whether this instance can dispatch to the specified end point.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        /// <returns><c>true</c> if this instance can dispatch to the specified end point; otherwise, <c>false</c>.</returns>
        bool CanDispatch(EndPointMetaData endPoint);

        /// <summary>
        /// Dispatches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<MessageResult> Dispatch(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null);

        Task<MessageResult> Dispatch(Request request, ExecutionContext context);
    }
}