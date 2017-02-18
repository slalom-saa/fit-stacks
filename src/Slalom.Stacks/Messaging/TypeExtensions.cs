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
            var actorType = type.GetBaseTypes().FirstOrDefault(
                e => IntrospectionExtensions.GetTypeInfo(e).IsGenericType && (e.GetGenericTypeDefinition() == typeof(Actor<,>) || e.GetGenericTypeDefinition() == typeof(Actor<>)));

            return actorType != null ? actorType.GetGenericArguments()[0] : null;
        }
    }
}