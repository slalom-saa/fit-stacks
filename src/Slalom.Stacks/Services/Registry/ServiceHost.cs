using System;
using System.Collections.Generic;
using System.Linq;

namespace Slalom.Stacks.Services.Registry
{
    public class ServiceHost
    {
        /// <summary>
        /// The local root path.
        /// </summary>
        public const string LocalPath = "stacks://local";

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; } = LocalPath;

        public List<ServiceMetaData> Services { get; set; } = new List<ServiceMetaData>();

        /// <summary>
        /// Creates a public service from this instance.
        /// </summary>
        /// <param name="path">The service path.</param>
        /// <returns>The created public service.</returns>
        public ServiceHost CreatePublicService(string path)
        {
            return new ServiceHost
            {
                Path = path,
                Services = this.Services.Where(e => e.Path != null).Select(e => e.Copy(path)).ToList()
            };
        }

        public void Add(Type service)
        {
            this.Services.Add(new ServiceMetaData(service, this.Path));
        }
    }
}