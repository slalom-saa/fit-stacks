using System;
using System.Net;
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

        private static string _localIpAddress;

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

        private static string GetLocalIPAddress()
        {
            if (_localIpAddress == null)
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        _localIpAddress = ip.ToString();
                    }
                }
            }
            return _localIpAddress;
        }
#else
        private string GetCorrelationId()
        {
            return NewId.NextId();
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntryAsync(Dns.GetHostName()).Result;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
#endif

        private ClaimsPrincipal GetWindowsClaimsPrincipal()
        {
            var target = WindowsIdentity.GetCurrent();
            return new ClaimsPrincipal(target);
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
                this.GetWindowsClaimsPrincipal(),
                GetLocalIPAddress(),
                Environment.MachineName,
                Environment.CurrentManagedThreadId);
        }
    }
}