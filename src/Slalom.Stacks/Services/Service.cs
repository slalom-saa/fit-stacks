using System.Collections.Generic;
using System.Linq;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// A service in the registry.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// The local root path.
        /// </summary>
        public const string LocalPath = "stacks://local";

        /// <summary>
        /// Gets or sets the end points.
        /// </summary>
        /// <value>The end points.</value>
        public List<EndPoint> EndPoints { get; set; } = new List<EndPoint>();

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; } = LocalPath;

        /// <summary>
        /// Creates a public service from this instance.
        /// </summary>
        /// <param name="path">The service path.</param>
        /// <returns>The created public service.</returns>
        public Service CreatePublicService(string path)
        {
            return new Service
            {
                Path = path,
                EndPoints = this.EndPoints.Where(e => e.Path != null).Select(e => e.Copy(path)).ToList()
            };
        }
    }
}