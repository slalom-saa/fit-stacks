using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Slalom.Stacks.Messaging.Registration
{
    public static class ServiceRegistryExtensions
    {
        public static ServiceRegistry GetServices(this Stack instance)
        {
            return instance.Container.Resolve<ServiceRegistry>();
        }

        public static ServiceRegistry CreatePublicRegistry(this Stack instance, string path)
        {
            return instance.Container.Resolve<ServiceRegistry>().CreatePublicRegistry(path);
        }   
    }
}
