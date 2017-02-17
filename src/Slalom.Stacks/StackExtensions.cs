using System;
using System.Collections.Generic;
using Autofac;
using System.Linq;
using Slalom.Stacks.Configuration;

#pragma warning disable 618

namespace Slalom.Stacks
{
    public static class StackExtensions
    {
        public static IEnumerable<T> ResolveAll<T>(this IComponentContext context)
        {
            return context.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Resolves all instance of the specified type from the container.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The resolved instances.</returns>
        /// <exception>Thrown when the <paramref name="type"/> argument is null.</exception>
        public static IEnumerable<object> ResolveAll(this IComponentContext context, Type type)
        {
            var target = ((IEnumerable<object>)context.Resolve(typeof(IEnumerable<>).MakeGenericType(type))).ToList();

            foreach (var instance in target)
            {
                context.InjectProperties(instance, AllUnsetPropertySelector.Instance);
            }

            return target;
        }

        public static IContainer Update(this IContainer instance, Action<ContainerBuilder> configuration)
        {
            var builder = new ContainerBuilder();
            configuration.Invoke(builder);
            builder.Update(instance.ComponentRegistry);
            return instance;
        }

        public static Stack Use(this Stack instance, Action<ContainerBuilder> configuration)
        {
            var builder = new ContainerBuilder();
            configuration.Invoke(builder);
            builder.Update(instance.Container.ComponentRegistry);
            return instance;
        }
    }
}