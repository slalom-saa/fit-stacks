using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Validation;
using System.Xml.XPath;
using Slalom.Stacks.Services;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Extensions for types within messaging.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the path for the endPoint.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns the path for the type.</returns>
        public static string GetPath(this Type type)
        {
            return type.GetAllAttributes<EndPoint>().Select(e => e.Path).FirstOrDefault();
        }

        /// <summary>
        /// Gets the version for the endPoint.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns the path for the type.</returns>
        public static int GetVersion(this Type type)
        {
            return type.GetAllAttributes<EndPoint>().FirstOrDefault()?.Version ?? 1;
        }

        private static ConcurrentDictionary<Assembly, XDocument> _commentsCache = new ConcurrentDictionary<Assembly, XDocument>();

        public static XDocument GetComments(this Assembly assembly)
        {
            return _commentsCache.GetOrAdd(assembly, a =>
            {
                var path = Path.Combine(Path.GetDirectoryName(a.Location), Path.GetFileNameWithoutExtension(a.Location) + ".xml");
                if (File.Exists(path))
                {
                    return XDocument.Load(path);
                }
                return null;
            });
        }

        public static string GetComments(this Type type)
        {
            var document = type.Assembly.GetComments();
            if (document != null)
            {
                var node = document.XPathSelectElement("//member[@name=\"T:" + type.FullName + "\"]");
                if (node != null)
                {
                    return node.XPathSelectElement("summary").Value.Trim();
                }
            }
            return null;
        }

        public static string GetComments(this PropertyInfo property)
        {
            var document = property.DeclaringType.Assembly.GetComments();
            if (document != null)
            {
                var node = document.XPathSelectElement("//member[@name=\"P:" + property.DeclaringType.FullName + "." + property.Name + "\"]");
                if (node != null)
                {
                    return node.XPathSelectElement("summary").Value.Trim();
                }
            }
            return null;
        }

        public static IEnumerable<EndPointProperty> GetInputProperties(this Type type)
        {
            type = type.GetRequestType();
            foreach (var property in type.GetProperties())
            {
                yield return new EndPointProperty(property);
            }
        }

        /// <summary>
        /// Gets the type of the request.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Type.</returns>
        public static Type GetRequestType(this Type type)
        {
            var actorType = type?.GetBaseAndContractTypes().FirstOrDefault(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(UseCase<>));

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