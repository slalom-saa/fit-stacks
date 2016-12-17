﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Slalom.Stacks.Reflection
{
    /// <summary>
    /// Contains extensions for <see cref="Type"/> classes.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns all custom attributes of the specified type.
        /// </summary>
        /// <typeparam name="T">The attribute type</typeparam>
        /// <param name="type">The instance.</param>
        /// <returns>Returns all custom attributes of the specified type.</returns>
        public static IEnumerable<T> GetAllAttributes<T>(this Type type) where T : Attribute
        {
            var target = new List<T>();
            do
            {
                target.AddRange(type.GetTypeInfo().GetCustomAttributes<T>());
                target.AddRange(type.GetInterfaces().SelectMany(e => e.GetAllAttributes<T>()));
                type = type.GetTypeInfo().BaseType;
            }
            while (type != null);

            return target.AsEnumerable();
        }

        /// <summary>
        /// Gets all base and contract types.
        /// </summary>
        /// <param name="type">The instance.</param>
        /// <returns>Returns all base and contract types.</returns>
        public static IEnumerable<Type> GetBaseAndContractTypes(this Type type)
        {
            return type.GetBaseTypes().Concat(type.GetInterfaces()).SelectMany(GetTypeAndGeneric).Where(t => t != type && t != typeof(object));
        }

        /// <summary>
        /// Gets all base types in the hierarchy.
        /// </summary>
        /// <param name="type">The instance.</param>
        /// <returns>Returns all base types in the hierarchy.</returns>
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            var currentType = type;
            while (currentType != null)
            {
                yield return currentType;
                currentType = currentType.GetTypeInfo().BaseType;
            }
        }

        /// <summary>
        /// Safely gets all types when some referenced assemblies may not be available.
        /// </summary>
        /// <param name="assemblies">The instance.</param>
        /// <returns>Returns all types when some referenced assemblies may not be available.</returns>
        public static Type[] SafelyGetTypes<T>(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(e => e.SafelyGetTypes()).Where(e => (typeof(T).IsAssignableFrom(e))).ToArray();
        }

        /// <summary>
        /// Safely gets all types when some referenced assemblies may not be available.
        /// </summary>
        /// <param name="assemblies">The instance.</param>
        /// <returns>Returns all types when some referenced assemblies may not be available.</returns>
        public static Type[] SafelyGetTypes(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(e => e.SafelyGetTypes()).ToArray();
        }

        /// <summary>
        /// Safely gets all types when some referenced assemblies may not be available.
        /// </summary>
        /// <param name="assembly">The instance.</param>
        /// <returns>Returns all types when some referenced assemblies may not be available.</returns>
        public static Type[] SafelyGetTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(e => e != null).ToArray();
            }
            catch (ReflectionTypeLoadException exception)
            {
                return exception.Types.Where(x => x != null).ToArray();
            }
            catch
            {
                return new Type[0];
            }
        }

        /// <summary>
        /// Gets all properties recursively.
        /// </summary>
        /// <param name="type">The root type.</param>
        /// <returns>All discovered properties.</returns>
        public static IEnumerable<PropertyInfo> GetPropertiesRecursive(this Type type)
        {
            var seenNames = new HashSet<string>();

            var currentTypeInfo = type.GetTypeInfo();

            while (currentTypeInfo.AsType() != typeof(object))
            {
                var unseenProperties = currentTypeInfo.DeclaredProperties.Where(p => p.CanRead &&
                                                                                     p.GetMethod.IsPublic &&
                                                                                     !p.GetMethod.IsStatic &&
                                                                                     (p.Name != "Item" || p.GetIndexParameters().Length == 0) &&
                                                                                     !seenNames.Contains(p.Name));

                foreach (var propertyInfo in unseenProperties)
                {
                    seenNames.Add(propertyInfo.Name);
                    yield return propertyInfo;
                }

                currentTypeInfo = currentTypeInfo.BaseType.GetTypeInfo();
            }
        }

        private static IEnumerable<Type> GetTypeAndGeneric(Type type)
        {
            yield return type;
            if (type.GetTypeInfo().IsGenericType && !type.GetTypeInfo().ContainsGenericParameters)
            {
                yield return type.GetGenericTypeDefinition();
            }
        }
    }
}