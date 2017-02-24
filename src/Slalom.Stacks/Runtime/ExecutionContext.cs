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
      

        public ExecutionContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


      


        [ThreadStatic] private static ExecutionContext _current;

        public ExecutionContext Resolve()
        {
            return _current ?? (_current = new ExecutionContext(_configuration?["Application"],
                _configuration?["Environment"],
                System.Environment.MachineName,
                System.Environment.CurrentManagedThreadId));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext" /> class.
        /// </summary>
        /// <param name="applicationName">The mame of the application.</param>
        /// <param name="environment">The current environment. (Development, Quality Assurance, Production)</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="threadId">The current thread identifier.</param>
        protected ExecutionContext(string applicationName, string environment, string machineName, int threadId)
        {
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
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; }
    }

    public interface IExecutionContext
    {
        ExecutionContext Resolve();
    }
}