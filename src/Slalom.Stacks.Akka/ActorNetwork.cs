using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Messaging
{
    public class ActorNetwork
    {
        private readonly ActorSystem _system;

        public ActorNetwork(ActorSystem system, IComponentContext context)
        {
            _system = system;
        }

        public async Task<CommandResult> Send(string path, string request)
        {
            var node = this.RootNode.Find(path);

            var command = (ICommand)JsonConvert.DeserializeObject(request, GetRequestType(node));

            var result = await _system.ActorSelection("user/" + node.Path).Ask(command);

            return result as CommandResult;
        }

        public async Task<CommandResult> Send(ICommand command)
        {
            var node = this.RootNode.Find(command);

            var result = await _system.ActorSelection("user/" + node.Path).Ask(command);

            return result as CommandResult;
        }

        private static Type GetRequestType(ActorNode node)
        {
            return node.Type.BaseType.GetGenericArguments()[0];
        }

        public ActorNode RootNode { get; private set; }

        public class ActorMapping
        {
            public string Path { get; set; }

            public Type Type { get; set; }

            public ActorMapping(string path, Type type)
            {
                Path = path;
                Type = type;
            }

            public ActorMapping()
            {
            }

            public static implicit operator ActorMapping(string value)
            {
                return new ActorMapping { Path = value };
            }
        }

        public void Arrange(Assembly[] assemblies)
        {
            var items = new List<ActorMapping>();
            var actors = assemblies.SafelyGetTypes(typeof(UseCaseActor<,>));
            foreach (var actor in actors)
            {
                var path = actor.GetCustomAttributes<PathAttribute>().FirstOrDefault()?.Path;
                if (path != null)
                {
                    items.Add(new ActorMapping(path, actor));
                }
            }

            RootNode = new ActorNode("root");
            PopulateActorNode(RootNode, items);

            foreach (var child in RootNode.Nodes)
            {
                if (child.Type == null)
                {
                    _system.ActorOf(_system.DI().Props<UseCaseSupervisionActor>(), child.Path);
                }
                else
                {
                    Console.WriteLine("ww");
                   // Context.ActorOf(Props.Create(() => new AkkaUseCaseActor(this.ComponentContext.Resolve(child.Type))), name);
                }
            }
        }

        private void PopulateActorNode(ActorNode parent, IEnumerable<ActorMapping> paths)
        {
            var items = paths.Select(e => new KeyValuePair<string[], Type>(e.Path.Split('/'), e.Type)).OrderBy(e => e.Key.Length);

            foreach (var item in items)
            {
                ActorNode current = parent;
                ActorNode last = parent;

                string sub = string.Empty;
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

        public Task<CommandResult> Send(string command, ICommand request)
        {
            throw new NotImplementedException();
        }
    }
}