using Autofac;

namespace Slalom.Stacks.Messaging.Registry
{
    public static class ServiceRegistryExtensions
    {
        public static ServiceRegistry GetServices(this Stack instance)
        {
            return instance.Container.Resolve<ServiceRegistry>();
        }
    }
}