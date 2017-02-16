using System;
using Autofac;
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
        public static void UseLocalCache(this Stack container)
        {
            Argument.NotNull(container, nameof(container));

            container.Container.Update(builder =>
            {
                builder.RegisterModule(new LocalCacheModule());
            });
        }
    }
}