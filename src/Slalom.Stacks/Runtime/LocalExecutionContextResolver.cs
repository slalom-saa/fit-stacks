using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Utilities.NewId;
using Slalom.Stacks.Validation;

#if !core
using System.Runtime.Remoting.Messaging;
#endif

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Resolves the message execution context containing information at the current time in processing. This information is otherwise lost
    /// when processing is multi-threaded or distributed.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Runtime.IExecutionContextResolver" />
    public class LocalExecutionContextResolver : IExecutionContextResolver
    {
        private const string Key = "CorrelationId";

        private static string session = NewId.NextId();
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalExecutionContextResolver"/> class.
        /// </summary>
        public LocalExecutionContextResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static string _sourceAddress;

#if !core
        private string GetCorrelationId()
        {
            if (CallContext.GetData(Key) == null)
            {
                var id = NewId.NextId();
                CallContext.SetData(Key, id);
            }
            return CallContext.GetData(Key).ToString();
        }
#else
        private string GetCorrelationId()
        {
            return NewId.NextId();
        }
#endif

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

        /// <summary>
        /// Resolves the current execution context.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public ExecutionContext Resolve()
        {
            return new ExecutionContext(_configuration?["Application"],
                _configuration?["Environment"],
                "",
                this.GetCorrelationId(),
                session,
                ClaimsPrincipal.Current,
                GetSourceIPAddress(),
                Environment.MachineName,
                Environment.CurrentManagedThreadId);
        }
    }
}