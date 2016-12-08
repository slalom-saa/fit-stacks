using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// Contains extension methods to update configuration settings.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Sets the configuration source to use appsettings.json, user secrets and environment variables.
        /// </summary>
        /// <param name="instance">The container instance.</param>
        /// <returns>Returns the container instance for method chaining.</returns>
        public static ApplicationContainer UseDeveloperSettings(this ApplicationContainer instance)
        {
            Argument.NotNull(() => instance);

            instance.Register(c =>
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddUserSecrets()
                    .AddEnvironmentVariables();
                return builder.Build();
            });
            return instance;
        }
    }
}