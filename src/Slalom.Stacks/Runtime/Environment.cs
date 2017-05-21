/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Contains information about the execution environment and a method to resolve that information.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Runtime.IEnvironmentContext" />
    public class Environment : IEnvironmentContext
    {
        [ThreadStatic]
        private static Environment current;

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
        /// <param name="version">The version number.</param>
        /// <param name="build">The build number.</param>
        protected Environment(string applicationName, string environment, string machineName, string version, string build)
        {
            this.MachineName = machineName;
            this.EnvironmentName = environment;
            this.ApplicationName = applicationName;
            this.Build = build;
            this.Version = version;
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName { get; }

        /// <summary>
        /// Gets the build number.
        /// </summary>
        /// <value>
        /// The build number.
        /// </value>
        public string Build { get; }

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
        /// Gets the version number.
        /// </summary>
        /// <value>
        /// The version number.
        /// </value>
        public string Version { get; }

        /// <inheritdoc />
        public Environment Resolve()
        {
            return current ?? (current = new Environment(_configuration?["Stacks:Application"],
                                   _configuration?["Stacks:Environment"],
                                   System.Environment.MachineName,
                                   _configuration?["Stacks:Version"],
                                   Assembly.GetEntryAssembly().GetName().Version.ToString()));
        }
    }
}