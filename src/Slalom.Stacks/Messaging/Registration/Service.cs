using System;

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
            this.Type = service.AssemblyQualifiedName;
            this.Input = service.GetRequestType().AssemblyQualifiedName;
            this.Output = service.GetResponseType()?.AssemblyQualifiedName;
        }

        /// <summary>
        /// Gets or sets the input type.
        /// </summary>
        /// <value>The input type.</value>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets the output type.
        /// </summary>
        /// <value>The output type.</value>
        public string Output { get; set; }

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

        /// <summary>
        /// Gets or sets the service type.
        /// </summary>
        /// <value>The service type.</value>
        public string Type { get; set; }
    }
}