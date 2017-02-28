using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slalom.Stacks.Serialization.Model;

namespace Slalom.Stacks.Messaging.Registration
{
    /// <summary>
    /// A service endpoint in the registry.
    /// </summary>
    public class EndPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndPoint"/> class.
        /// </summary>
        /// <param name="endpoint">The endPoint type.</param>
        public EndPoint(Type endpoint)
        {
            this.Path = endpoint.GetPath();
            this.Type = endpoint.AssemblyQualifiedName;
            this.RequestType = endpoint.GetRequestType().AssemblyQualifiedName;
            this.ResponseType = endpoint.GetResponseType()?.AssemblyQualifiedName;
            this.Rules = endpoint.GetRules().Select(e => e.Name).ToList();
            this.Version = endpoint.GetVersion();
            this.RequestProperties = endpoint.GetInputProperties().ToList();
            this.Summary = endpoint.GetComments();
        }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the input properties.
        /// </summary>
        /// <value>The input properties.</value>
        public List<ServiceProperty> RequestProperties { get; set; }

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
        /// Gets or sets the relative path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        public List<string> Rules { get; set; }

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

        public EndPoint Copy()
        {
            return (EndPoint) this.MemberwiseClone();
        }
    }
}