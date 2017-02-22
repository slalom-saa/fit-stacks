using System;
using System.Reflection;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Modules;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Contains methods to configure a <see cref="Stack"/>.
    /// </summary>
    public static class MessagingExtensions
    {
        public static Task Send(this Stack instance, string path, ICommand command, TimeSpan? timeout = null)
        {
            return instance.Container.Resolve<IMessageStream>().Send(command, timeout: timeout);
        }

        /// <summary>
        /// Adds messaging types found in the specified type assemblies.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="types">The types to use to get the assemblies.</param>
        /// <returns>The current instance for method chaining.</returns>
        public static Stack AddMessagingTypes(this Stack instance, params Type[] types)
        {
            instance.Use(builder =>
            {
                builder.RegisterModule(new MessagingTypesModule(types.Select(e => e.GetTypeInfo().Assembly).ToArray()));
            });
            return instance;
        }
    }
}