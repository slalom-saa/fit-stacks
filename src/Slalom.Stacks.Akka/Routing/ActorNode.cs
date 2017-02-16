using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Messaging.Routing
{
    public static class TypeExtensions
    {
        public static Type GetRequestType(this Type type)
        {
            var actorType = type.GetBaseTypes().FirstOrDefault(
                e => e.IsGenericType && e.GetGenericTypeDefinition() == typeof(UseCaseActor<,>));

            return actorType != null ? actorType.GetGenericArguments()[0] : null;
        }
    }

    public class ActorNode
    {
        public ActorNode(string path, Type type = null)
        {
            this.Path = path;
            this.Type = type;
            
            this.RequestType = type.GetRequestType();
           
        }

        public List<ActorNode> Nodes { get; } = new List<ActorNode>();

        public string Path { get; }

        public Type Type { get; }

        public Type RequestType { get; }

        public ActorNode Add(string path, Type type)
        {
            var target = new ActorNode(path, type);
            this.Nodes.Add(target);
            return target;
        }

        public ActorNode Find(string path)
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

        public ActorNode Find(ICommand command)
        {
            if (this.RequestType == command.GetType())
            {
                return this;
            }
            foreach (var node in this.Nodes)
            {
                var result = node.Find(command);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}