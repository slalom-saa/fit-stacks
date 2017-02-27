using System;
using System.Linq;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Indicates the path the service.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class PathAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PathAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public PathAttribute(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The name.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>The version number.</value>
        public int Version { get; set; } = 1;
    }
}