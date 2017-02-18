using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Validation;

#pragma warning disable 618

namespace Slalom.Stacks
{
    /// <summary>
    /// Extensions for <see cref="Stack" /> instances.
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Resolves all instance of the specified type from the container.
        /// </summary>
        /// <returns>The resolved instances.</returns>
        /// <exception>Thrown when the <paramref name="instance" /> argument is null.</exception>
        public static IEnumerable<T> ResolveAll<T>(this IComponentContext instance)
        {
            Argument.NotNull(instance, nameof(instance));

            return instance.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Resolves all instance of the specified type from the container.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The resolved instances.</returns>
        /// <exception>Thrown when the <paramref name="instance" /> argument is null.</exception>
        /// <exception>Thrown when the <paramref name="type" /> argument is null.</exception>
        public static IEnumerable<object> ResolveAll(this IComponentContext instance, Type type)
        {
            Argument.NotNull(instance, nameof(instance));
            Argument.NotNull(type, nameof(type));

            var target = ((IEnumerable<object>) instance.Resolve(typeof(IEnumerable<>).MakeGenericType(type))).ToList();

            foreach (var item in target)
            {
                instance.InjectProperties(item, AllProperties.Instance);
            }

            return target;
        }

        /// <summary>
        /// Updates the container with the specified builder configuration.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="configuration">The configuration routine.</param>
        /// <returns>The current instance for method chaining.</returns>
        /// <exception>Thrown when the <paramref name="instance" /> argument is null.</exception>
        /// <exception>Thrown when the <paramref name="configuration" /> argument is null.</exception>
        public static IContainer Update(this IContainer instance, Action<ContainerBuilder> configuration)
        {
            Argument.NotNull(instance, nameof(instance));
            Argument.NotNull(configuration, nameof(configuration));

            var builder = new ContainerBuilder();
            configuration.Invoke(builder);
            builder.Update(instance.ComponentRegistry);
            return instance;
        }

        /// <summary>
        /// Tells the stack to use the specified builder configuration.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="configuration">The configuration routine.</param>
        /// <returns>The current instance for method chaining.</returns>
        /// <exception>Thrown when the <paramref name="instance" /> argument is null.</exception>
        /// <exception>Thrown when the <paramref name="configuration" /> argument is null.</exception>
        public static Stack Use(this Stack instance, Action<ContainerBuilder> configuration)
        {
            Argument.NotNull(instance, nameof(instance));
            Argument.NotNull(configuration, nameof(configuration));

            var builder = new ContainerBuilder();
            configuration.Invoke(builder);
            builder.Update(instance.Container.ComponentRegistry);
            return instance;
        }
    }
}