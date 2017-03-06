using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Services.Registry
{
    /// <summary>
    /// A service endpoint in the registry.
    /// </summary>
    public class EndPointMetaData
    {
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
        /// Gets or sets the input properties.
        /// </summary>
        /// <value>The input properties.</value>
        public List<EndPointProperty> RequestProperties { get; set; }

        /// <summary>
        /// Gets or sets the input type.
        /// </summary>
        /// <value>The input type.</value>
        public string RequestType { get; set; }

        /// <summary>
        /// Gets or sets the output type.
        /// </summary>
        /// <value>The output type.</value>
        public string ResponseType { get; set; }

        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        /// <value>The root path.</value>
        public string RootPath { get; set; }

        public List<EndPointRule> Rules { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the endpoint timeout.
        /// </summary>
        /// <value>The endpoint timeout.</value>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets or sets the endPoint type.
        /// </summary>
        /// <value>The endPoint type.</value>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>The version number.</value>
        public int Version { get; set; }

        /// <summary>
        /// Copies this instance using the specified root path.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <returns>Returns the copied instance.</returns>
        public EndPointMetaData Copy(string rootPath)
        {
            var target = (EndPointMetaData)this.MemberwiseClone();
            target.RootPath = rootPath;
            return target;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndPointMetaData" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="rootPath">The root path.</param>
        /// <returns>EndPoint.</returns>
        public static IEnumerable<EndPointMetaData> Create(Type service, string rootPath = ServiceHost.LocalPath)
        {
            var interfaces = service.GetInterfaces().Where(e => e.IsGenericType && e.GetGenericTypeDefinition() == typeof(IHandle<>));
            if (interfaces.Any())
            {
                var path = service.GetPath();
                var version = service.GetVersion();
                var summary = service.GetComments();
                var timeout = service.GetTimeout();

                foreach (var item in interfaces)
                {
                    var method = item.GetMethod("Handle");
                    var requestType = method.GetParameters().FirstOrDefault()?.ParameterType;

                    var endPoint = new EndPointMetaData
                    {
                        Path = path,
                        ServiceType = service.AssemblyQualifiedName,
                        RequestType = requestType?.AssemblyQualifiedName,
                        ResponseType = method.ReturnType.AssemblyQualifiedName,
                        Rules = requestType?.GetRules().Select(e => new EndPointRule { Name = e.Name }).ToList(),
                        Version = version,
                        RequestProperties = requestType?.GetInputProperties().ToList(),
                        Summary = summary,
                        RootPath = rootPath,
                        Timeout = timeout
                    };
                    yield return endPoint;
                }
            }
        }
    }
}