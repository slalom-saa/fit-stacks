using System;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Caching
{
    /// <summary>
    /// Contains configuration extensions for caching blocks.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configures the container to use a local cache.
        /// </summary>
        /// <param name="container">The current container.</param>
        public static void UseLocalCache(this ApplicationContainer container)
        {
            Argument.NotNull(container, nameof(container));

            container.RegisterModule(new LocalCacheModule());
        }
    }
}