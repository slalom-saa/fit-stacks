using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Messaging;
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
        public List<ServiceHost> Services { get; set; } = new List<ServiceHost>();

        /// <summary>
        /// Creates a public registry from the current service registry.
        /// </summary>
        /// <param name="path">The path of the public service.</param>
        /// <returns>The created public registry.</returns>
        public ServiceRegistry CreatePublicRegistry(string path)
        {
            var target = new ServiceRegistry();
            foreach (var service in this.Services.Where(e => e.Path == ServiceHost.LocalPath))
            {
                target.Services.Add(service.CreatePublicService(path));
            }
            return target;
        }

        /// <summary>
        /// Finds services based on the specified endPoint.
        /// </summary>
        /// <param name="message">The endPoint.</param>
        /// <returns>Services that are registered to take the specified endPoint.</returns>
        public IEnumerable<EndPointMetaData> Find(IMessage message)
        {
            return this.Services.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).Where(e => e.RequestType == message.MessageType.AssemblyQualifiedName);
        }

        /// <summary>
        /// Finds the endPoint registered at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the endPoint registered at the specified path.</returns>
        public EndPointMetaData Find(string path)
        {
            var target = this.Services.SelectMany(e => e.Services).SelectMany(e=>e.EndPoints).FirstOrDefault(e => $"v{e.Version}/{e.Path}" == path);
            if (target == null)
            {
                target = this.Services.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).Where(e => e.Path == path).OrderBy(e => e.Version).LastOrDefault();
            }

            return target;
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
                    //collection.EndPoints.AddRange(EndPoint.Create(service));
                }
            }
            this.Services.Add(host);
        }

        public void IncludeRemoteServices(string path, ServiceRegistry remote)
        {
            foreach (var service in remote.CreatePublicRegistry(path).Services)
            {
                this.Services.Add(service);
            }
        }

        public EndPointMetaData Find(Type endPoint)
        {
            return null;
            //return this.Services.SelectMany(e => e.EndPoints).FirstOrDefault(e => e.Type == endPoint.AssemblyQualifiedName);
        }

        public EndPointMetaData Find(string path, IMessage instance)
        {
            if (path != null)
            {
                return this.Find(path);
            }
            else
            {
                return this.Find(instance).OrderBy(e => e.Version).LastOrDefault();
            }
        }
    }
}