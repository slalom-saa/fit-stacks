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
        /// Gets an optional setting from the configuration.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The default value.</param>
        /// <returns>Returns the found setting, the default value or null.</returns>
        public static string GetOptionalSetting(this IConfiguration instance, string name, string value = null)
        {
            var target = instance[name];
            if (!string.IsNullOrWhiteSpace(target))
            {
                return target;
            }
            return value;
        }

        /// <summary>
        /// Sets the configuration source to use appsettings.json, user secrets and environment variables.
        /// </summary>
        /// <param name="instance">The container instance.</param>
        /// <returns>Returns the container instance for method chaining.</returns>
        public static ApplicationContainer UseDeveloperSettings(this ApplicationContainer instance)
        {
            Argument.NotNull(instance, nameof(instance));

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