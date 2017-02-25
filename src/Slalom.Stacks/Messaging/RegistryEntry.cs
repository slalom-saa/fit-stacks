using System;
using System.Collections.Generic;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An entry that has a path, actor type and request type.
    /// </summary>
    public class RegistryEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryEntry"/> class.
        /// </summary>
        /// <param name="path">The actor path.</param>
        /// <param name="type">The actor type.</param>
        /// <param name="parent">The parent.</param>
        public RegistryEntry(string path, Type type = null, RegistryEntry parent = null)
        {
            this.Path = path;
            this.Type = type;
            this.RequestType = type.GetRequestType();
            this.Parent = parent;
        }

        /// <summary>
        /// Gets the child entries.
        /// </summary>
        /// <value>The child entries.</value>
        public List<RegistryEntry> Children { get; } = new List<RegistryEntry>();

        /// <summary>
        /// Gets the parent entry.
        /// </summary>
        /// <value>The parent entry.</value>
        public RegistryEntry Parent { get; }

        /// <summary>
        /// Gets the actor path.
        /// </summary>
        /// <value>The actor path.</value>
        public string Path { get; }

        /// <summary>
        /// Gets the type of the request.
        /// </summary>
        /// <value>The type of the request.</value>
        public Type RequestType { get; }

        /// <summary>
        /// Gets the actor type.
        /// </summary>
        /// <value>The actor type.</value>
        public Type Type { get; }

        /// <summary>
        /// Adds an entry with the specified information
        /// </summary>
        /// <param name="path">The actor path.</param>
        /// <param name="type">The actor type.</param>
        /// <returns>Returns the added entry.</returns>
        public RegistryEntry Add(string path, Type type)
        {
            var target = new RegistryEntry(path, type, this);
            this.Children.Add(target);
            return target;
        }

        /// <summary>
        /// Finds the entry with the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the entry with the specified path.</returns>
        public RegistryEntry Find(string path)
        {
            if (this.Path == path)
            {
                return this;
            }
            foreach (var node in this.Children)
            {
                var result = node.Find(path);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the entry with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns the entry with the specified message.</returns>
        public IEnumerable<RegistryEntry> Find(IMessage message)
        {
            if (this.RequestType == message.GetType())
            {
                yield return this;
            }
            foreach (var node in this.Children)
            {
                var result = node.Find(message);
                foreach (var entry in result)
                {
                    yield return entry;
                }
            }
        }
    }
}