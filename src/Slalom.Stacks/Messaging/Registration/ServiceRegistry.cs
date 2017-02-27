using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Messaging.Registration
{
    public class ServiceRegistry
    {
        public List<Service> Services { get; set; } = new List<Service>();

        public IEnumerable<Service> Find(IMessage message)
        {
            return this.Services.Where(e => e.Input == message.GetType().AssemblyQualifiedName);
        }

        public Service Find(string path)
        {
            return this.Services.FirstOrDefault(e => e.Path == path);
        }

        public void RegisterLocal(Assembly[] assemblies)
        {
            foreach (var service in assemblies.SafelyGetTypes(typeof(IHandle<>)))
            {
                if (!service.IsGenericType && !service.IsDynamic())
                {
                    this.Services.Add(new Service(service, "stacks://local"));
                }
            }
        }
    }
}