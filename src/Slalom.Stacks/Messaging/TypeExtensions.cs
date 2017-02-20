using System;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Messaging
{
    public static class TypeExtensions
    {
        public static Type GetRequestType(this Type type)
        {
            var actorType = type?.GetInterfaces().FirstOrDefault(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IHandle<>));

            return actorType != null ? actorType.GetGenericArguments()[0] : null;
        }
    }
}