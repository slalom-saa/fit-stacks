using System;
using System.Linq;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Used to define the path to an actor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PathAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PathAttribute"/> class.
        /// </summary>
        /// <param name="path">The path to the actor.</param>
        public PathAttribute(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// Gets the path to the actor.
        /// </summary>
        /// <value>The path to the actor.</value>
        public string Path { get; }
    }
}