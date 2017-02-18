using System;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Messaging
{
    internal class HandlerMapping
    {
        public HandlerMapping(string path, Type type)
        {
            this.Path = path;
            if (type.GetTypeInfo().IsGenericType && type.GetInterfaces().Any(e => e == typeof(IHandle)))
            {
                this.Type = type;
            }
            else
            {
                this.Type = type;
            }
        }

        public string Path { get; set; }

        public Type Type { get; set; }
    }
}
