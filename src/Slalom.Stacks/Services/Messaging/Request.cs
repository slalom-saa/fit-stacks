/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// Represents a request being made to an endpoint.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Services.Messaging.IRequestContext" />
    public class Request : IRequestContext
    {
        private static string _sourceAddress;

        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        public string CorrelationId { get; private set; }

        /// <summary>
        /// Gets the request message.
        /// </summary>
        /// <value>The request message.</value>
        public IMessage Message { get; private set; }

        /// <summary>
        /// Gets the parent context.
        /// </summary>
        /// <value>The parent context.</value>
        public Request Parent { get; private set; }

        /// <summary>
        /// Gets the requested path.
        /// </summary>
        /// <value>The requested path.</value>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the user's session identifier.
        /// </summary>
        /// <value>The user's session identifier.</value>
        public string SessionId { get; private set; }

        /// <summary>
        /// Gets the calling IP address.
        /// </summary>
        /// <value>The calling IP address.</value>
        public string SourceAddress { get; private set; }

        /// <summary>
        /// Gets the user making the request.
        /// </summary>
        /// <value>The user making the request.</value>
        [JsonConverter(typeof(ClaimsPrincipalConverter))]
        public ClaimsPrincipal User { get; private set; }

        /// <inheritdoc />
        public Request Resolve(object message, EndPointMetaData endPoint, Request parent = null)
        {
            return new Request
            {
                CorrelationId = this.GetCorrelationId(),
                SourceAddress = this.GetSourceIPAddress(),
                SessionId = this.GetSession(),
                User = this.GetUser(),
                Parent = parent,
                Path = endPoint.Path,
                Message = this.GetMessage(message, endPoint)
            };
        }

        /// <inheritdoc />
        public Request Resolve(string path, object message, Request parent = null)
        {
            return new Request
            {
                CorrelationId = this.GetCorrelationId(),
                SourceAddress = this.GetSourceIPAddress(),
                SessionId = this.GetSession(),
                User = this.GetUser(),
                Parent = parent,
                Path = path ?? message?.GetType().GetAllAttributes<RequestAttribute>().FirstOrDefault()?.Path,
                Message = this.GetMessage(path, message)
            };
        }

        /// <inheritdoc />
        public Request Resolve(string command, EndPointMetaData endPoint, Request parent = null)
        {
            return new Request
            {
                CorrelationId = this.GetCorrelationId(),
                SourceAddress = this.GetSourceIPAddress(),
                SessionId = this.GetSession(),
                User = this.GetUser(),
                Parent = parent,
                Path = endPoint.Path,
                Message = this.GetMessage(command, endPoint)
            };
        }

        /// <inheritdoc />
        public Request Resolve(EventMessage instance, Request parent)
        {
            return new Request
            {
                CorrelationId = parent.CorrelationId,
                SourceAddress = parent.SourceAddress,
                SessionId = parent.SessionId,
                User = parent.User,
                Parent = parent,
                Message = instance
            };
        }

        /// <summary>
        /// Gets the current correlation ID.
        /// </summary>
        /// <returns>Returns the current correlation ID.</returns>
        protected virtual string GetCorrelationId()
        {
            return NewId.NextId();
        }

        /// <summary>
        /// Gets the current session ID.
        /// </summary>
        /// <returns>Returns the current session ID.</returns>
        protected virtual string GetSession()
        {
            return NewId.NextId();
        }

        /// <summary>
        /// Gets the source IP address.
        /// </summary>
        /// <returns>Returns the source IP address.</returns>
        protected virtual string GetSourceIPAddress()
        {
            if (_sourceAddress == null)
            {
                //try
                //{
                //    using (var client = new HttpClient())
                //    {
                //        var response = client.GetAsync("http://ipinfo.io/ip").Result;
                //        sourceAddress = response.Content.ReadAsStringAsync().Result.Trim();
                //    }
                //}
                //catch
                //{
                _sourceAddress = "127.0.0.1";
                //}
            }
            return _sourceAddress;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>Returns the current user.</returns>
        protected virtual ClaimsPrincipal GetUser()
        {
            return ClaimsPrincipal.Current;
        }

        private IMessage GetMessage(string path, object message)
        {
            if (message is IMessage)
            {
                return (IMessage) message;
            }
            return new Message(message);
        }

        private IMessage GetMessage(string message, EndPointMetaData endPoint)
        {
            if (message == null)
            {
                if (endPoint.RequestType != typeof(object))
                {
                    return new Message(JsonConvert.DeserializeObject("{}", endPoint.RequestType));
                }
                return new Message();
            }
            return new Message(JsonConvert.DeserializeObject(message, endPoint.RequestType));
        }

        private IMessage GetMessage(object message, EndPointMetaData endPoint)
        {
            if (message != null && message.GetType() == endPoint.RequestType)
            {
                return new Message(message);
            }
            if (message != null)
            {
                var content = JsonConvert.SerializeObject((message as EventMessage)?.Body ?? message);
                return new Message(JsonConvert.DeserializeObject(content, endPoint.RequestType));
            }
            return new Message(JsonConvert.DeserializeObject("{}", endPoint.RequestType));
        }
    }
}