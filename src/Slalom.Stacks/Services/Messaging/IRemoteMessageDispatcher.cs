/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// Dispatches messages to remote endpoints.
    /// </summary>
    public interface IRemoteMessageDispatcher
    {
        /// <summary>
        /// Determines whether this instance can dispatch the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if this instance can dispatch the specified request; otherwise, <c>false</c>.</returns>
        bool CanDispatch(Request request);

        /// <summary>
        /// Dispatches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task<MessageResult> Dispatch(Request request, ExecutionContext parentContext, TimeSpan? timeout = null);
    }
}