using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// A service endpoint in the registry.
    /// </summary>
    public class EndPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndPoint" /> class.
        /// </summary>
        /// <param name="endpoint">The endpoint type.</param>
        /// <param name="rootPath">The root path.</param>
        public EndPoint(Type endpoint, string rootPath = Service.LocalPath)
        {
            this.Path = endpoint.GetPath();
            this.Type = endpoint.AssemblyQualifiedName;
            this.RequestType = endpoint.GetRequestType().AssemblyQualifiedName;
            this.ResponseType = endpoint.GetResponseType()?.AssemblyQualifiedName;
            this.Rules = endpoint.GetRules().Select(e => new EndPointRule { Name = e.Name }).ToList();
            this.Version = endpoint.GetVersion();
            this.RequestProperties = endpoint.GetInputProperties().ToList();
            this.Summary = endpoint.GetComments();
            this.RootPath = rootPath;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is local.
        /// </summary>
        /// <value><c>true</c> if this instance is local; otherwise, <c>false</c>.</value>
        public bool IsLocal => this.RootPath == Service.LocalPath;

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
        /// Gets or sets the endPoint type.
        /// </summary>
        /// <value>The endPoint type.</value>
        public string Type { get; set; }

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
        public EndPoint Copy(string rootPath)
        {
            var target = (EndPoint)this.MemberwiseClone();
            target.RootPath = rootPath;
            return target;
        }
    }
}