using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Messaging.Registration
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
        public List<Service> Services { get; set; } = new List<Service>();

        /// <summary>
        /// Finds services based on the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Services that are registered to take the specified message.</returns>
        public IEnumerable<Service> Find(IMessage message)
        {
            return this.Services.Where(e => e.InputType == message.GetType().AssemblyQualifiedName);
        }

        /// <summary>
        /// Finds the service registered at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the service registered at the specified path.</returns>
        public Service Find(string path)
        {
            var target = this.Services.FirstOrDefault(e => $"v{e.Version}/{e.Path}" == path);
            if (target == null)
            {
                target = this.Services.Where(e => e.Path == path).OrderBy(e => e.Version).LastOrDefault();
            }

            return target;
        }

        /// <summary>
        /// Registers local services.
        /// </summary>
        /// <param name="assemblies">The assemblies to use to search.</param>
        public void RegisterLocal(Assembly[] assemblies)
        {
            foreach (var service in assemblies.SafelyGetTypes(typeof(IHandle<>)))
            {
                if (!service.IsGenericType && !service.IsDynamic())
                {
                    this.Services.Add(new Service(service, Service.LocalPath));
                }
            }
        }
    }
}