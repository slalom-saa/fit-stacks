using System;
using System.Collections.Generic;

namespace Slalom.Stacks.Services.Inventory
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


        public void Add(Type service)
        {
            this.Services.Add(new ServiceMetaData(service, this.Path));
        }
    }
}