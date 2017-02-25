using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// A registry containing all local types that handle messages and their paths.
    /// </summary>
    public class LocalRegistry
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalRegistry"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies to load from.</param>
        public LocalRegistry(Assembly[] assemblies)
        {
            _assemblies = assemblies;

            this.Build();
        }

        /// <summary>
        /// Gets the root entry.
        /// </summary>
        /// <value>The root entry.</value>
        public RegistryEntry Root { get; private set; }

        /// <summary>
        /// Finds the entry with the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the entry with the specified path.</returns>
        public RegistryEntry Find(string path)
        {
            return this.Root.Find(path);
        }

        /// <summary>
        /// Finds the entry with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns the entry with the specified message.</returns>
        public IEnumerable<RegistryEntry> Find(IMessage message)
        {
            return this.Root.Find(message);
        }

        private void Build()
        {
            var items = new List<RegistryEntry>();
            var actors = _assemblies.SafelyGetTypes(typeof(IHandle<>));

            foreach (var actor in actors)
            {
                var path = actor.GetTypeInfo().GetCustomAttributes<PathAttribute>().FirstOrDefault()?.Path;
                items.Add(new RegistryEntry(path, actor));
            }

            this.Root = new RegistryEntry("root", null);
            this.PopulateActorNode(this.Root, items);
        }

        private void PopulateActorNode(RegistryEntry parent, IEnumerable<RegistryEntry> paths)
        {
            var i = 0;
            foreach (var source in paths.Where(e => e.Path == null))
            {
                parent.Add(source.Type.Name.ToDelimited("-") + "-$" + i++, source.Type);
            }

            var items = paths.Where(e => e.Path != null).Select(e => new KeyValuePair<string[], Type>(e.Path.Split('/'), e.Type)).OrderBy(e => e.Key.Length);

            foreach (var item in items)
            {
                var current = parent;
                var last = parent;

                var sub = string.Empty;
                foreach (var part in item.Key)
                {
                    sub += part;
                    current = current.Find(sub);
                    if (current == null)
                    {
                        if (part == item.Key.Last())
                        {
                            current = last.Add(sub, item.Value);
                        }
                        else
                        {
                            current = last.Add(sub, null);
                        }
                    }
                    last = current;
                    sub += "/";
                }
            }
        }
    }
}