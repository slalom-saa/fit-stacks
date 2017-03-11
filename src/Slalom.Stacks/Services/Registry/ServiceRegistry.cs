using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Services.Registry
{
    /// <summary>
    /// A simple service registration implementation.
    /// </summary>
    public class ServiceRegistry
    {
        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>The services.</value>
        public List<ServiceHost> Hosts { get; set; } = new List<ServiceHost>();

        /// <summary>
        /// Creates a public registry from the current service registry.
        /// </summary>
        /// <param name="path">The path of the public service.</param>
        /// <returns>The created public registry.</returns>
        public ServiceRegistry CreatePublicRegistry(string path)
        {
            var target = new ServiceRegistry();
            foreach (var service in this.Hosts.Where(e => e.Path == ServiceHost.LocalPath))
            {
                target.Hosts.Add(service.CreatePublicService(path));
            }
            return target;
        }

        public IEnumerable<EndPointMetaData> Find(Command command)
        {
            if (command == null)
            {
                return Enumerable.Empty<EndPointMetaData>();
            }
            return this.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).Where(e => e.RequestType == command.GetType().AssemblyQualifiedName);
        }

        /// <summary>
        /// Finds the endPoint registered at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the endPoint registered at the specified path.</returns>
        public EndPointMetaData Find(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var target = this.Hosts.SelectMany(e => e.Services).SelectMany(e=>e.EndPoints).FirstOrDefault(e => $"v{e.Version}/{e.Path}" == path);
            if (target == null)
            {
                target = this.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).Where(e => e.Path == path).OrderBy(e => e.Version).LastOrDefault();
            }
            return target;
        }

        public IEnumerable<EndPointMetaData> Find(EventMessage instance)
        {
            return this.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).Where(e => e.RequestType == instance.MessageType.AssemblyQualifiedName);
        }

        /// <summary>
        /// Registers local services.
        /// </summary>
        /// <param name="assemblies">The assemblies to use to search.</param>
        public void RegisterLocal(Assembly[] assemblies)
        {
            var host = new ServiceHost();
            foreach (var service in assemblies.SafelyGetTypes(typeof(IEndPoint<>)))
            {
                if (!service.IsGenericType && !service.IsDynamic())
                {
                    host.Add(service);
                }
            }
            this.Hosts.Add(host);
        }

        public void Include(ServiceRegistry remote)
        {
            foreach (var host in remote.Hosts)
            {
                this.Hosts.Add(host);
            }
        }

        public EndPointMetaData Find(string path, Command command)
        {
            var target = this.Find(path);
            if (command != null)
            {
                if (target == null)
                {
                    target = this.Find(command).FirstOrDefault();
                }
                if (target == null)
                {
                    var attribute = command.GetType().GetAllAttributes<CommandAttribute>().FirstOrDefault();
                    if (attribute != null)
                    {
                        target = this.Find(attribute.Path);
                    }
                }
            }
            return target;
        }
    }
}