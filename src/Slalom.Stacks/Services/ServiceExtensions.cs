using Autofac;

namespace Slalom.Stacks.Services
{
    public static class ServiceRegistryExtensions
    {
        public static ServiceRegistry CreatePublicRegistry(this Stack instance, string path)
        {
            return instance.Container.Resolve<ServiceRegistry>().CreatePublicRegistry(path);
        }

        public static ServiceRegistry GetServices(this Stack instance)
        {
            return instance.Container.Resolve<ServiceRegistry>();
        }
    }
}