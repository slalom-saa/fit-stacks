using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Text;

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
            this.EndPointType = endpoint.AssemblyQualifiedName;
            this.RequestType = endpoint.GetRequestType().AssemblyQualifiedName;
            this.ResponseType = endpoint.GetResponseType()?.AssemblyQualifiedName;
            this.Rules = endpoint.GetRules().Select(e => new EndPointRule(e)).ToList();
            this.Version = endpoint.GetVersion();
            this.RequestProperties = endpoint.GetInputProperties().ToList();
            this.ResponseProperties = endpoint.GetOutputProperties().ToList();
            this.Summary = endpoint.GetComments();
            this.RootPath = rootPath;
        }

        public string RootPath { get; set; }

        public bool IsLocal => this.RootPath == Service.LocalPath;

        /// <summary>
        /// Gets or sets the relative path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the request properties.
        /// </summary>
        /// <value>The request properties.</value>
        public List<EndPointProperty> RequestProperties { get; set; }

        /// <summary>
        /// Gets or sets the request type.
        /// </summary>
        /// <value>The request type.</value>
        public string RequestType { get; set; }

        public string RequestName
        {
            get { return this.RequestType.Split(',')[0].Split('.').Last(); }
        }

        public string ResponseName
        {
            get { return ResponseType?.Split(',')[0].Split('.').Last(); }
        }

        /// <summary>
        /// Gets or sets the output type.
        /// </summary>
        /// <value>The output type.</value>
        public string ResponseType { get; set; }

        /// <summary>
        /// Gets or sets the response properties.
        /// </summary>
        /// <value>The response properties.</value>
        public List<EndPointProperty> ResponseProperties { get; set; }

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
        public string EndPointType { get; set; }

        public string Name
        {
            get
            {
                var type = Type.GetType(this.EndPointType, false);
                if (type == null)
                {
                    return this.EndPointType.Split(',')[0].Split('.').Last().ToTitle();
                }
                return type.Name.ToTitle();
            }
        }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>The version number.</value>
        public int Version { get; set; }

        public EndPoint Copy(string rootPath)
        {
            var target = (EndPoint)this.MemberwiseClone();
            target.RootPath = rootPath;
            return target;
        }

        public IMessage CreateMessage(string content)
        {
            return (IMessage)JsonConvert.DeserializeObject(content, System.Type.GetType(this.RequestType));
        }
    }
}