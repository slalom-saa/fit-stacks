using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Messaging
{
    public class LocalRegistryEntry
    {
        public LocalRegistryEntry(string path, Type type = null, LocalRegistryEntry parent = null)
        {
            this.Path = path;
            this.Type = type;
            this.RequestType = type.GetRequestType();
            this.Parent = parent;
        }

        public LocalRegistryEntry Parent { get; }

        public string Path { get; }

        public Type Type { get; }

        public Type RequestType { get; }

        public List<LocalRegistryEntry> Nodes { get; } = new List<LocalRegistryEntry>();

        public LocalRegistryEntry Add(string path, Type type)
        {
            var target = new LocalRegistryEntry(path, type, this);
            this.Nodes.Add(target);
            return target;
        }

        public LocalRegistryEntry Find(string path)
        {
            if (this.Path == path)
            {
                return this;
            }
            foreach (var node in this.Nodes)
            {
                var result = node.Find(path);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public IEnumerable<LocalRegistryEntry> Find(IMessage message)
        {
            if (this.RequestType == message.GetType())
            {
                yield return this;
            }
            foreach (var node in this.Nodes)
            {
                var result = node.Find(message);
                foreach (var entry in result)
                {
                    yield return entry;
                }
            }
        }
    }

    public class Registry
    {
        private readonly Assembly[] _assemblies;

        public Registry(Assembly[] assemblies)
        {
            _assemblies = assemblies;

            this.Build();
        }

        public LocalRegistryEntry Root { get; private set; }

        public LocalRegistryEntry Find(string path)
        {
            return this.Root.Find(path);
        }

        public IEnumerable<LocalRegistryEntry> Find(IMessage message)
        {
            return this.Root.Find(message);
        }

        private void Build()
        {
            var items = new List<LocalRegistryEntry>();
            var actors = _assemblies.SafelyGetTypes(typeof(IHandle<>));

            foreach (var actor in actors)
            {
                var path = actor.GetTypeInfo().GetCustomAttributes<PathAttribute>().FirstOrDefault()?.Path;
                items.Add(new LocalRegistryEntry(path, actor));
            }

            this.Root = new LocalRegistryEntry("root", null);
            this.PopulateActorNode(this.Root, items);
        }

        private void PopulateActorNode(LocalRegistryEntry parent, IEnumerable<LocalRegistryEntry> paths)
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