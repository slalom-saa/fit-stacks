/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using Slalom.Stacks.Validation;
using Microsoft.Extensions.Configuration;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Contains information about the execution environment and a method to resolve that information.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Runtime.IEnvironmentContext" />
    public class Environment : IEnvironmentContext
    {
        [ThreadStatic] private static Environment _current;

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Runtime.Environment" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Environment(IConfiguration configuration)
        {
            Argument.NotNull(configuration, nameof(configuration));

            _configuration = configuration;
        }

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
            return _current ?? (_current = new Environment(_configuration?["Stacks:Application"],
                       _configuration?["Stacks:Environment"],
                       System.Environment.MachineName,
                       System.Environment.CurrentManagedThreadId));
        }
    }
}