using System;
using System.Reflection;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Modules;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Contains methods to configure a <see cref="Stack"/>.
    /// </summary>
    public static class MessagingExtensions
    {
        /// <summary>
        /// Sends the specified command to the configured point-to-point endPoint.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Task<MessageResult> Send(this Stack instance, Command command, TimeSpan? timeout = null)
        {
            return instance.Container.Resolve<IMessageGateway>().Send(command, timeout: timeout);
        }

        public static Stack UseInMemoryRequestLogging(this Stack instance)
        {
            instance.Use(builder =>
            {
                builder.RegisterType<InMemoryRequestLog>().As<IRequestLog>().SingleInstance();
                builder.RegisterType<InMemoryResponseLog>().As<IResponseLog>().SingleInstance();
            });
            return instance;
        }

        /// <summary>
        /// Sends the specified command to the configured point-to-point endPoint.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="path">The path.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Task<MessageResult> Send(this Stack instance, string path, Command command, TimeSpan? timeout = null)
        {
            return instance.Container.Resolve<IMessageGateway>().Send(path, command, timeout: timeout);
        }

        /// <summary>
        /// Sends the an empty command to the configured point-to-point endPoint.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="path">The path.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Task<MessageResult> Send(this Stack instance, string path, TimeSpan? timeout = null)
        {
            return instance.Container.Resolve<IMessageGateway>().Send(path, (Command)null, timeout: timeout);
        }

        /// <summary>
        /// Sends the specified command to the configured point-to-point endPoint.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="path">The path to the receiver.</param>
        /// <param name="command">The serialized command to send.</param>
        /// <param name="timeout">The request timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Task<MessageResult> Send(this Stack instance, string path, string command, TimeSpan? timeout = null)
        {
            return instance.Container.Resolve<IMessageGateway>().Send(path, command, timeout: timeout);
        }
    }
}