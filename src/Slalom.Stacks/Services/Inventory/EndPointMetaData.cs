/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Services.Inventory
{
    /// <summary>
    /// A service endpoint in the registry.
    /// </summary>
    public class EndPointMetaData
    {
        /// <summary>
        /// Gets or sets the endpoint type.
        /// </summary>
        /// <value>The endpoint type.</value>
        public Type EndPointType { get; set; }

        /// <summary>
        /// Gets or sets the endpoint method.
        /// </summary>
        /// <value>The endpoint method.</value>
        public MethodInfo InvokeMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is an older version.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is an older version; otherwise, <c>false</c>.
        /// </value>
        public bool IsVersioned { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method.
        /// </summary>
        /// <value>
        /// The HTTP method.
        /// </value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the endpoint name.
        /// </summary>
        /// <value>
        /// The endpoint name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the relative path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the endpoint should be public.
        /// </summary>
        /// <value><c>true</c> if public; otherwise, <c>false</c>.</value>
        public bool Public { get; set; }

        /// <summary>
        /// Gets or sets the request type.
        /// </summary>
        /// <value>The request type.</value>
        public Type RequestType { get; set; }

        /// <summary>
        /// Gets or sets the response type.
        /// </summary>
        /// <value>The response type.</value>
        public Type ResponseType { get; set; }

        /// <summary>
        /// Gets or sets the rules.
        /// </summary>
        /// <value>The rules.</value>
        public List<EndPointRule> Rules { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the endpoint is secure.
        /// </summary>
        /// <value>Indicates whether the endpoint is secure.</value>
        public bool Secure { get; set; }

        /// <summary>
        /// Gets or sets the endpoint summary.
        /// </summary>
        /// <value>The endpoint summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the endpoint tags.
        /// </summary>
        /// <value>
        /// The endpoint tags.
        /// </value>
        public string[] Tags { get; set; }

        /// <summary>
        /// Gets or sets the endpoint timeout.
        /// </summary>
        /// <value>The endpoint timeout.</value>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets or sets the endpoint version number.
        /// </summary>
        /// <value>The endpoint version number.</value>
        public int Version { get; set; }

        /// <summary>
        /// Creates endpoint metadata for the specified service.
        /// </summary>
        /// <param name="service">The owning service.</param>
        /// <returns>Returns endpoint metadata for the specified service.</returns>
        public static IEnumerable<EndPointMetaData> Create(Type service)
        {
            var interfaces = service.GetInterfaces().Where(e => e.GetTypeInfo().IsGenericType && (e.GetGenericTypeDefinition() == typeof(IEndPoint<>) || e.GetGenericTypeDefinition() == typeof(IEndPoint<,>))).ToList();
            if (interfaces.Any())
            {
                var path = service.GetPath();
                var version = service.GetVersion();
                var summary = service.GetComments();
                var timeout = service.GetTimeout();

                foreach (var item in interfaces)
                {
                    var method = item.GetMethod("Receive");
                    if (method.DeclaringType != null)
                    {
                        var attribute = service.GetAllAttributes<EndPointAttribute>().FirstOrDefault();

                        var requestType = method.GetParameters().FirstOrDefault()?.ParameterType;
                        var endPoint = new EndPointMetaData
                        {
                            Name = attribute?.Name ?? service.Name.ToTitle(),
                            Path = path,
                            Method = attribute?.Method ?? "POST",
                            EndPointType = service,
                            RequestType = requestType,
                            Tags = attribute?.Tags,
                            ResponseType = GetResponseType(method),
                            Rules = requestType?.GetRules().Select(e => new EndPointRule(e)).ToList(),
                            Version = version,
                            Summary = summary?.Summary,
                            Timeout = timeout,
                            InvokeMethod = method,
                            Public = service.GetAllAttributes<EndPointAttribute>().FirstOrDefault()?.Public ?? true,
                            Secure = attribute?.Secure ?? false
                        };
                        yield return endPoint;
                    }
                }
            }
        }

        private static Type GetResponseType(MethodInfo method)
        {
            if (method.ReturnType == typeof(Task))
            {
                return null;
            }
            if ((bool)method.ReturnType?.GetTypeInfo().IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return method.ReturnType.GetGenericArguments()[0];
            }
            return method.ReturnType;
        }
    }
}