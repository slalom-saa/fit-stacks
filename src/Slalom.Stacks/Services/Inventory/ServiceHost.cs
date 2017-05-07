/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;

namespace Slalom.Stacks.Services.Inventory
{
    /// <summary>
    /// Represents the host for services.
    /// </summary>
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

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>The services.</value>
        public List<ServiceMetaData> Services { get; set; } = new List<ServiceMetaData>();


        /// <summary>
        /// Adds the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        public void Add(Type service)
        {
            this.Services.Add(new ServiceMetaData(service, this.Path));
        }
    }
}