using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Newtonsoft.Json;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    public class Request : IRequestContext
    {
        private static string sourceAddress;

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
                User = ClaimsPrincipal.Current,
                Parent = parent,
                Path = endPoint.Path,
                Message = this.GetMessage(message, endPoint)
            };
        }

        private IMessage GetMessage(object message, EndPointMetaData endPoint)
        {
            if (message is IMessage)
            {
                return (IMessage)message;
            }

            if (message == null)
            {
                return new Message(JsonConvert.DeserializeObject("{}", Type.GetType(endPoint.RequestType)));
            }
            if (message.GetType().AssemblyQualifiedName == endPoint.RequestType)
            {
                return new Message(message);
            }
            if (message is String && endPoint.RequestType != typeof(String).AssemblyQualifiedName)
            {
                if (String.IsNullOrWhiteSpace((string)message))
                {
                    message = "{}";
                }
                return new Message(JsonConvert.DeserializeObject((string)message, Type.GetType(endPoint.RequestType)));
            }
            else
            {
                return new Message(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(message), Type.GetType(endPoint.RequestType)));
            }
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
            if (sourceAddress == null)
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
                    sourceAddress = "127.0.0.1";
                //}
            }
            return sourceAddress;
        }

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
    }
}