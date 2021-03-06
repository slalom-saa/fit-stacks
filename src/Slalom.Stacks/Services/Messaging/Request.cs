﻿/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System.Security.Claims;
using Newtonsoft.Json;
using Slalom.Stacks.Serialization;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// Represents a request being made to an endpoint.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        public string CorrelationId { get; internal set; }

        /// <summary>
        /// Gets the request message.
        /// </summary>
        /// <value>The request message.</value>
        public IMessage Message { get; internal set; }

        /// <summary>
        /// Gets the parent context.
        /// </summary>
        /// <value>The parent context.</value>
        public Request Parent { get; internal set; }

        /// <summary>
        /// Gets the requested path.
        /// </summary>
        /// <value>The requested path.</value>
        public string Path { get; internal set; }

        /// <summary>
        /// Gets the user's session identifier.
        /// </summary>
        /// <value>The user's session identifier.</value>
        public string SessionId { get; internal set; }

        /// <summary>
        /// Gets the calling IP address.
        /// </summary>
        /// <value>The calling IP address.</value>
        public string SourceAddress { get; internal set; }

        /// <summary>
        /// Gets the user making the request.
        /// </summary>
        /// <value>The user making the request.</value>
        [JsonConverter(typeof(ClaimsPrincipalConverter))]
        public ClaimsPrincipal User { get; internal set; }
    }
}