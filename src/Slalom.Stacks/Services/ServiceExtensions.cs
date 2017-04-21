using Autofac;
using Slalom.Stacks.Services.Inventory;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// Service extention methods for Stacks.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Gets the local service inventory.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Slalom.Stacks.Services.Inventory.ServiceInventory.</returns>
        public static ServiceInventory GetServices(this Stack instance)
        {
            return instance.Container.Resolve<ServiceInventory>();
        }
    }
}