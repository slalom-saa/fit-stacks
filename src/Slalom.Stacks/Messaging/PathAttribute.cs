using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Indicates the path the actor.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class PathAttribute : Attribute
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The name.</value>
        public string Path { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public PathAttribute(string path)
        {
            this.Path = path;
        }
    }
}
