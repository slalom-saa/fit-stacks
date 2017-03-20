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
    /// A service registry containing all registered services.
    /// </summary>
    public class ServiceRegistry
    {
        /// <summary>
        /// Gets the registered services.
        /// </summary>
        /// <value>The registered services.</value>
        public List<ServiceHost> Hosts { get; } = new List<ServiceHost>();

        /// <summary>
        /// Finds the endpoint for the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns the endpoint for the specified message.</returns>
        public IEnumerable<EndPointMetaData> Find(object message)
        {
            if (message == null)
            {
                return Enumerable.Empty<EndPointMetaData>();
            }
            return this.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).Where(e => e.RequestType == message.GetType());
        }

        /// <summary>
        /// Finds the endpoint for the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the endpoint for the specified path.</returns>
        public EndPointMetaData Find(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var endPoints = this.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints);
            var target = endPoints.FirstOrDefault(e => $"v{e.Version}/{e.Path}" == path);
            if (target == null)
            {
                target = endPoints.Where(e => e.Path == path).OrderBy(e => e.Version).LastOrDefault();
            }
            return target;
        }

        /// <summary>
        /// Finds the endpoint for the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns the endpoint for the specified message.</returns>
        public IEnumerable<EndPointMetaData> Find(EventMessage message)
        {
            return this.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).Where(e => e.RequestType == message.MessageType);
        }

        /// <summary>
        /// Finds the endpoint that can handle the specified path.  If there is no path, then the message will be used.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="message">The message.</param>
        /// <returns>Returns the endpoint.</returns>
        public EndPointMetaData Find(string path, object message)
        {
            var target = this.Find(path);
            if (message != null)
            {
                if (target == null)
                {
                    target = this.Find(message).FirstOrDefault();
                }
                if (target == null)
                {
                    var attribute = message.GetType().GetAllAttributes<CommandAttribute>().FirstOrDefault();
                    if (attribute != null)
                    {
                        target = this.Find(attribute.Path);
                    }
                }
            }
            return target;
        }

        /// <summary>
        /// Registers all local services using the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to use to scan.</param>
        public void RegisterLocal(Assembly[] assemblies)
        {
            var host = new ServiceHost();
            foreach (var service in assemblies.SafelyGetTypes(typeof(IEndPoint)).Distinct())
            {
                if (!service.IsGenericType && !service.IsDynamic() && !service.IsAbstract)
                {
                    host.Add(service);
                }
            }
            this.Hosts.Add(host);
        }
    }
}