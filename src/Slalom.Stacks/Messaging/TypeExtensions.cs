using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Linq;
using Slalom.Stacks.Messaging.Registration;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Extensions for types within messaging.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the path for the service.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns the path for the type.</returns>
        public static string GetPath(this Type type)
        {
            return type.GetAllAttributes<PathAttribute>().Select(e => e.Path).FirstOrDefault();
        }

        /// <summary>
        /// Gets the version for the service.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns the path for the type.</returns>
        public static int GetVersion(this Type type)
        {
            return type.GetAllAttributes<PathAttribute>().FirstOrDefault()?.Version ?? 1;
        }

        public static IEnumerable<ServiceProperty> GetInputProperties(this Type type)
        {
            type = type.GetRequestType();
            foreach (var property in type.GetProperties())
            {
                yield return new ServiceProperty(property.Name, property.PropertyType);
            }
        }

        /// <summary>
        /// Gets the type of the request.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Type.</returns>
        public static Type GetRequestType(this Type type)
        {
            var actorType = type?.GetInterfaces().FirstOrDefault(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IHandle<>));

            return actorType != null ? actorType.GetGenericArguments()[0] : null;
        }

        /// <summary>
        /// Gets the type of the response.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The response type.</returns>
        public static Type GetResponseType(this Type type)
        {
            var actorType = type?.GetBaseAndContractTypes().FirstOrDefault(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(UseCase<,>));

            return actorType != null ? actorType.GetGenericArguments()[1] : null;
        }

        public static Type[] GetRules(this Type type)
        {
            var input = type.GetRequestType();

            return type.Assembly.SafelyGetTypes(typeof(IValidate<>).MakeGenericType(input));
        }

        /// <summary>
        /// Determines whether the type handles commands.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the type handles commands; otherwise, <c>false</c>.</returns>
        public static bool IsCommandHandler(this Type type)
        {
            return typeof(ICommand).IsAssignableFrom(type.GetRequestType());
        }

        /// <summary>
        /// Determines whether the specified type is dynamic.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is dynamic; otherwise, <c>false</c>.</returns>
        public static bool IsDynamic(this Type type)
        {
            return typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type);
        }
    }
}