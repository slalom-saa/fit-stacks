using System;
using System.Collections.Generic;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// Context that contains current components from the container.
    /// </summary>
    public interface IComponentContext
    {
        /// <summary>
        /// Resolves an instance of the speciifed type from the context.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The resolved instance.</returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolves all instance of the specified type from the container.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The resolved instances.</returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// Resolves an instance of the speciifed type from the context.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>The resolved instance.</returns>
        T Resolve<T>();
    }
}