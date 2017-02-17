//using System;
//using System.Collections.Generic;

//namespace Slalom.Stacks.Configuration
//{
//    /// <summary>
//    /// Context that registers and resolves components.
//    /// </summary>
//    public interface IComponentContext
//    {
//        /// <summary>
//        /// Builds up the specified instance.
//        /// </summary>
//        /// <typeparam name="T">The type of instance</typeparam>
//        /// <param name="instance">The instance.</param>
//        /// <returns>The newly built up instance.</returns>
//        T BuildUp<T>(T instance);

//        /// <summary>
//        /// Resolves an instance of the specified type from the context.
//        /// </summary>
//        /// <param name="type">The type to resolve.</param>
//        /// <returns>The resolved instance.</returns>
//        object Resolve(Type type);

//        /// <summary>
//        /// Resolves an instance of the specified type from the context.
//        /// </summary>
//        /// <typeparam name="T">The type to resolve.</typeparam>
//        /// <returns>The resolved instance.</returns>
//        T Resolve<T>();

//        /// <summary>
//        /// Resolves all instance of the specified type from the container.
//        /// </summary>
//        /// <param name="type">The type to resolve.</param>
//        /// <returns>The resolved instances.</returns>
//        IEnumerable<object> ResolveAll(Type type);

//        /// <summary>
//        /// Resolves all instances of the specified type.
//        /// </summary>
//        /// <typeparam name="T">The type to resolve</typeparam>
//        /// <returns>Returns all instances of the specified type.</returns>
//        IEnumerable<T> ResolveAll<T>();
//    }
//}