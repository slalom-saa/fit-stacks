using System;
using System.Dynamic;
using System.Reflection;
using System.Linq;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Extensions for types within messaging.
    /// </summary>
    public static class TypeExtensions
    {
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

        public static Type GetResponseType(this Type type)
        {
            var actorType = type?.GetBaseAndContractTypes().FirstOrDefault(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(UseCase<,>));

            return actorType != null ? actorType.GetGenericArguments()[1] : null;
        }

        public static bool IsCommandHandler(this Type type)
        {
            return typeof(ICommand).IsAssignableFrom(type.GetRequestType());
        }

        public static string GetPath(this Type type)
        {
            return type.GetAllAttributes<PathAttribute>().Select(e => e.Path).FirstOrDefault();
        }

        public static bool IsDynamic(this Type type)
        {
            return typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type);
        }
    }
}