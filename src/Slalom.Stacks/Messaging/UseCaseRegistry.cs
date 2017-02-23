using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Reflection;
using System.Reflection;

namespace Slalom.Stacks.Messaging
{
    public class UseCaseRegistry
    {
        public class UseCaseRegistryEntry
        {
            public string Path { get; }

            public Type Type { get; }

            public Type RequestType { get; }

            public List<UseCaseRegistryEntry> Nodes { get; } = new List<UseCaseRegistryEntry>();

            public UseCaseRegistryEntry(string path, Type type = null)
            {
                this.Path = path;
                this.Type = type;

                this.RequestType = type.GetRequestType();

            }

            public UseCaseRegistryEntry Add(string path, Type type)
            {
                var target = new UseCaseRegistryEntry(path, type);
                this.Nodes.Add(target);
                return target;
            }

            public UseCaseRegistryEntry Find(string path)
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

            public UseCaseRegistryEntry Find(IMessage message)
            {
                if (this.RequestType == message.GetType())
                {
                    return this;
                }
                foreach (var node in this.Nodes)
                {
                    var result = node.Find(message);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
        }

        public void Build(Assembly[] assemblies)
        {
            var items = new List<UseCaseRegistryEntry>();
            var actors = assemblies.SafelyGetTypes(typeof(IHandle<>));

            foreach (var actor in actors)
            {
                var path = actor.GetTypeInfo().GetCustomAttributes<PathAttribute>().FirstOrDefault()?.Path;
                items.Add(new UseCaseRegistryEntry(path, actor));
            }

            this._rootNode = new UseCaseRegistryEntry("root");
            this.PopulateActorNode(this._rootNode, items);
        }

        private UseCaseRegistryEntry _rootNode;

        private void PopulateActorNode(UseCaseRegistryEntry parent, IEnumerable<UseCaseRegistryEntry> paths)
        {
            foreach (var source in paths.Where(e => e.Path == null))
            {
                parent.Add(source.Type.Name, source.Type);
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

        public UseCaseRegistryEntry Find(string path)
        {
            return this._rootNode.Find(path);
        }

        public UseCaseRegistryEntry Find(IMessage message)
        {
            return this._rootNode.Find(message);
        }
    }
}
