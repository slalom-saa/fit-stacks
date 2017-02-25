﻿using System;
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
        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="path">The path.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Task Send(this Stack instance, string path, ICommand command, TimeSpan? timeout = null)
        {
            return instance.Container.Resolve<IMessageGatewayAdapter>().Send(path, command, timeout: timeout);
        }

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="path">The path to the receiver.</param>
        /// <param name="command">The serialized command to send.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Task Send(this Stack instance, string path, string command, TimeSpan? timeout = null)
        {
            return instance.Container.Resolve<IMessageGatewayAdapter>().Send(path, command, timeout: timeout);
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