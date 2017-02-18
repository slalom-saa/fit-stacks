using System;
using System.Reflection;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Search;

namespace Slalom.Stacks
{
    /// <summary>
    /// The host and main entry point to the stack.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class Stack : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stack"/> class.
        /// </summary>
        /// <param name="markers">Item markers used to identify assemblies.</param>
        public Stack(params object[] markers)
        {
            this.Assemblies = markers.Select(e =>
            {
                var type = e as Type;
                if (type != null)
                {
                    return type.GetTypeInfo().Assembly;
                }
                var assembly = e as Assembly;
                if (assembly != null)
                {
                    return assembly;
                }
                return e.GetType().GetTypeInfo().Assembly;
            }).Distinct().ToArray();

            var builder = new ContainerBuilder();

            builder.RegisterModule(new ConfigurationModule(this.Assemblies));

            this.Container = builder.Build();
        }

        /// <summary>
        /// Gets the assemblies that are used for loading components.
        /// </summary>
        /// <value>The assemblies that are used for loading components.</value>
        public Assembly[] Assemblies { get; }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/>.</value>
        public IDomainFacade Domain => this.Container.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/>.</value>
        public ISearchFacade Search => this.Container.Resolve<ISearchFacade>();

        /// <summary>
        /// Gets the configured <see cref="ILogger"/>.
        /// </summary>
        /// <value>The configured <see cref="ILogger"/>.</value>
        public ILogger Logger => this.Container.Resolve<ILogger>();

        /// <summary>
        /// Gets the configured <see cref="IContainer"/>.
        /// </summary>
        public IContainer Container { get; }

        /// <summary>
        /// Sends the specified command with the specified timeout.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return this.Container.Resolve<ICommandCoordinator>().SendAsync(command, timeout);
        }

        /// <summary>
        /// Sends the specified command with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<CommandResult> SendAsync(string path, ICommand command, TimeSpan? timeout = null)
        {
            return this.Container.Resolve<ICommandCoordinator>().SendAsync(path, command, timeout);
        }

        /// <summary>
        /// Sends the specified command with the specified timeout.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="command">The command to send.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null)
        {
            return this.Container.Resolve<ICommandCoordinator>().SendAsync(path, command, timeout);
        }

        #region IDisposable Implementation

        bool _disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Stack"/> class.
        /// </summary>
        ~Stack()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
                this.Container.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion
    }
}