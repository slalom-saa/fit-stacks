using System;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Represents the environment request and contains information about machine execution. This information is otherwise lost
    /// when processing is multi-threaded or distributed.
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        private readonly IConfiguration _configuration;
        private static string _sourceAddress;

        public ExecutionContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetCorrelationId()
        {
            return NewId.NextId();
        }

        private string GetSession()
        {
            return NewId.NextId();
        }

        private static string GetSourceIPAddress()
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

        [ThreadStatic] private static ExecutionContext _current;

        public ExecutionContext Resolve()
        {
            return _current ?? (_current = new ExecutionContext(_configuration?["Application"],
                _configuration?["Environment"],
                this.GetCorrelationId(),
                this.GetSession(),
                ClaimsPrincipal.Current,
                GetSourceIPAddress(),
                System.Environment.MachineName,
                System.Environment.CurrentManagedThreadId));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext" /> class.
        /// </summary>
        /// <param name="applicationName">The mame of the application.</param>
        /// <param name="environment">The current environment. (Development, Quality Assurance, Production)</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="user">The current user.</param>
        /// <param name="sourceAddress">The user host address.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="threadId">The current thread identifier.</param>
        protected ExecutionContext(string applicationName, string environment, string correlationId, string sessionId, ClaimsPrincipal user, string sourceAddress, string machineName, int threadId)
        {
            this.CorrelationId = correlationId;
            this.SessionId = sessionId;
            this.User = user;
            this.SourceAddress = sourceAddress;
            this.MachineName = machineName;
            this.Environment = environment;
            this.ApplicationName = applicationName;
            this.ThreadId = threadId;
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName { get; }

        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        public string CorrelationId { get; }

        /// <summary>
        /// Gets the name of the environment.
        /// </summary>
        /// <value>The name of the environment.</value>
        public string Environment { get; }

        /// <summary>
        /// Gets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; }

        /// <summary>
        /// Gets a null execution request.
        /// </summary>
        /// <value>A null execution request.</value>
        public static ExecutionContext Null => new NullExecutionContext();

        /// <summary>
        /// Gets the user's session identifier.
        /// </summary>
        /// <value>The user's session identifier.</value>
        public string SessionId { get; }

        /// <summary>
        /// Gets the calling IP address.
        /// </summary>
        /// <value>The calling IP address.</value>
        public string SourceAddress { get; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; }

        /// <summary>
        /// Gets the user making the request.
        /// </summary>
        /// <value>The user making the request.</value>
        [JsonConverter(typeof(ClaimsPrincipalConverter))]
        public ClaimsPrincipal User { get; }
    }

    public interface IExecutionContext
    {
        ExecutionContext Resolve();
    }
}