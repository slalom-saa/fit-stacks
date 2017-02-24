using System.Net.Http;
using System.Security.Claims;
using Newtonsoft.Json;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    public class RequestContext : IRequestContext
    {
        private static string _sourceAddress;

        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        public string CorrelationId { get; private set; }

        public IMessage Message { get; private set; }

        public string Path { get; private set; }

        public string MessageId { get; private set; }

        public string MessageName { get; private set; }

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

        public RequestContext Resolve(string requestName, string path, IMessage message, RequestContext parent = null)
        {
            return new RequestContext()
            {
                CorrelationId = this.GetCorrelationId(),
                SourceAddress = this.GetSourceIPAddress(),
                SessionId = this.GetSession(),
                User = ClaimsPrincipal.Current,
                MessageName = requestName,
                MessageId = message.Id,
                Path = path,
                Message = message,
                ParentContext = parent
            };
        }

        public RequestContext ParentContext { get; private set; }

        protected virtual string GetSourceIPAddress()
        {
            if (_sourceAddress == null)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync("http://ipinfo.io/ip").Result;
                        _sourceAddress = response.Content.ReadAsStringAsync().Result.Trim();
                    }
                }
                catch
                {
                    _sourceAddress = "127.0.0.1";
                }
            }
            return _sourceAddress;
        }


        private string GetCorrelationId()
        {
            return NewId.NextId();
        }

        private string GetSession()
        {
            return NewId.NextId();
        }
    }

    public interface IRequestContext
    {
        RequestContext Resolve(string requestName, string path, IMessage message, RequestContext parentContext = null);
    }
}