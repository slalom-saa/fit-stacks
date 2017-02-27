using System;
using System.Collections.Generic;
using System.Linq;

namespace Slalom.Stacks.Messaging.Registration
{
    /// <summary>
    /// A service registration in the registry.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// The local root path.
        /// </summary>
        public const string LocalPath = "stacks://local";

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="rootPath">The root path.</param>
        public Service(Type service, string rootPath)
        {
            this.Path = service.GetPath();
            this.RootPath = rootPath;
            this.ServiceType = service.AssemblyQualifiedName;
            this.InputType = service.GetRequestType().AssemblyQualifiedName;
            this.OutputType = service.GetResponseType()?.AssemblyQualifiedName;
            this.Rules = service.GetRules().Select(e => e.Name).ToList();
            this.Version = service.GetVersion();
            this.InputProperties = service.GetInputProperties().ToList();
        }

        /// <summary>
        /// Gets or sets the input properties.
        /// </summary>
        /// <value>The input properties.</value>
        public List<ServiceProperty> InputProperties { get; set; }

        /// <summary>
        /// Gets or sets the input type.
        /// </summary>
        /// <value>The input type.</value>
        public string InputType { get; set; }

        /// <summary>
        /// Gets or sets the output type.
        /// </summary>
        /// <value>The output type.</value>
        public string OutputType { get; set; }

        /// <summary>
        /// Gets or sets the relative path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        /// <value>The root path.</value>
        public string RootPath { get; set; }

        public List<string> Rules { get; set; }

        /// <summary>
        /// Gets or sets the service type.
        /// </summary>
        /// <value>The service type.</value>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>The version number.</value>
        public int Version { get; set; }

        /// <summary>
        /// Determines whether the service is local.
        /// </summary>
        /// <returns><c>true</c> if the service is local; otherwise, <c>false</c>.</returns>
        public bool IsLocal()
        {
            return this.RootPath == LocalPath;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{this.RootPath}/v{this.Version}/{this.Path}] [{this.ServiceType}]";
        }
    }
}