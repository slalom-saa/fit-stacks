using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Services.Registry
{
    /// <summary>
    /// A service endpoint in the registry.
    /// </summary>
    public class EndPointMetaData
    {
        /// <summary>
        /// Gets or sets the endpoint method.
        /// </summary>
        /// <value>The endpoint method.</value>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is local.
        /// </summary>
        /// <value><c>true</c> if this instance is local; otherwise, <c>false</c>.</value>
        public bool IsLocal => this.RootPath == ServiceHost.LocalPath;

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
        /// Gets or sets the request properties.
        /// </summary>
        /// <value>The request properties.</value>
        public List<EndPointProperty> RequestProperties { get; set; }

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
        /// Gets or sets the root path.
        /// </summary>
        /// <value>The root path.</value>
        public string RootPath { get; set; }

        /// <summary>
        /// Gets or sets the rules.
        /// </summary>
        /// <value>The rules.</value>
        public List<EndPointRule> Rules { get; set; }

        /// <summary>
        /// Gets or sets the service type.
        /// </summary>
        /// <value>The service type.</value>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the endpoint summary.
        /// </summary>
        /// <value>The endpoint summary.</value>
        public string Summary { get; set; }

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
        /// <param name="rootPath">The root path.</param>
        /// <returns>Returns endpoint metadata for the specified service.</returns>
        public static IEnumerable<EndPointMetaData> Create(Type service, string rootPath = ServiceHost.LocalPath)
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
                        var map = service.GetInterfaceMap(method.DeclaringType);
                        var index = Array.IndexOf(map.InterfaceMethods, method);
                        var m = map.TargetMethods[index];
                        var attribute = m?.GetCustomAttributes<EndPointAttribute>().FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(attribute?.Path))
                        {
                            path = attribute.Path;
                        }

                        var requestType = method.GetParameters().FirstOrDefault()?.ParameterType;
                        var endPoint = new EndPointMetaData
                        {
                            Path = path,
                            ServiceType = service,
                            RequestType = requestType,
                            ResponseType = method.ReturnType,
                            Rules = requestType?.GetRules().Select(e => new EndPointRule(e)).ToList(),
                            Version = version,
                            RequestProperties = requestType?.GetInputProperties().ToList(),
                            Summary = summary?.Summary,
                            RootPath = rootPath,
                            Timeout = timeout,
                            Method = method,
                            Public = service.GetAllAttributes<EndPointAttribute>().FirstOrDefault()?.Public ?? true
                        };
                        yield return endPoint;
                    }
                }
            }
        }
    }
}