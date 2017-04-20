using System;
using Slalom.Stacks.Validation;

#if core
using Microsoft.Extensions.Configuration;
#else
using System.Configuration;
#endif

namespace Slalom.Stacks.Runtime
{
    public class Environment : IEnvironmentContext
    {
        [ThreadStatic]
        private static Environment current;

#if core
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Runtime.Environment"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Environment(IConfiguration configuration)
        {
            Argument.NotNull(configuration, nameof(configuration));

            _configuration = configuration;
        }
#else
        internal Environment()
        {
            
        }

#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="Runtime.Environment" /> class.
        /// </summary>
        /// <param name="applicationName">The mame of the application.</param>
        /// <param name="environment">The current environment. (Development, Quality Assurance, Production)</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="threadId">The current thread identifier.</param>
        protected Environment(string applicationName, string environment, string machineName, int threadId)
        {
            this.MachineName = machineName;
            this.EnvironmentName = environment;
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
        public string EnvironmentName { get; }

        /// <summary>
        /// Gets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; }

        /// <summary>
        /// Gets a null execution request.
        /// </summary>
        /// <value>A null execution request.</value>
        public static Environment Null => new NullEnvironment();

        /// <summary>
        /// Gets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; }

        /// <inheritdoc />
        public Environment Resolve()
        {
#if core
            return current ?? (current = new Environment(_configuration?["Application"],
                                   _configuration?["Environment"],
                                   System.Environment.MachineName,
                                   System.Environment.CurrentManagedThreadId));
#else
            return current ?? (current = new Environment(ConfigurationManager.AppSettings["Application"],
                       ConfigurationManager.AppSettings["Environment"],
                       System.Environment.MachineName,
                       System.Environment.CurrentManagedThreadId));
#endif
        }
    }
}