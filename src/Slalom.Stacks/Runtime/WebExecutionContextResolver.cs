using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Resolves the message execution context containing information at the current time in processing. This information is otherwise lost
    /// when processing is multi-threaded or distributed.
    /// </summary>
    public class WebExecutionContextResolver : IExecutionContextResolver
    {
        private const string Key = "CorrelationId";
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebExecutionContextResolver"/> class.
        /// </summary>
        /// <param name="contextAccessor">The configured <see cref="IHttpContextAccessor" /> instance.</param>
        /// <param name="configuration">The current <see cref="IConfiguration"/> instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="configuration"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="contextAccessor"/> argument is null.</exception>
        public WebExecutionContextResolver(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            Argument.NotNull(configuration, nameof(configuration));
            Argument.NotNull(contextAccessor, nameof(contextAccessor));

            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Resolves the current execution context.
        /// </summary>
        /// <returns>Returns the current execution context.</returns>
        public ExecutionContext Resolve()
        {
            var httpContext = _contextAccessor.HttpContext;
            var httpRequest = httpContext.Request;

            return new ExecutionContext(_configuration["Application"],
                _configuration["Environment"],
                httpContext.Connection.LocalIpAddress.ToString(),
                httpRequest != null ? $"{httpRequest.Method} {httpRequest.Scheme}://{httpRequest.Host.Value}{httpRequest.Path}{httpRequest.QueryString}" : null,
                this.GetCorrelationId(),
                httpRequest?.Headers.FirstOrDefault(e => e.Key == "Session").Value,
                httpContext.User,
                httpContext.Connection.RemoteIpAddress.ToString(),
                Environment.MachineName,
                Environment.CurrentManagedThreadId);
        }

        private string GetCorrelationId()
        {
            if (_contextAccessor.HttpContext.Items[Key] == null)
            {
                _contextAccessor.HttpContext.Items[Key] = Guid.NewGuid().ToString();
            }
            return (string)_contextAccessor.HttpContext.Items[Key];
        }
    }
}