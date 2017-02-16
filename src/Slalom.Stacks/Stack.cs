using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks
{
    public class Stack : IDisposable
    {
        public IContainer Container { get; }

        public Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return this.Container.Resolve<ICommandCoordinator>().SendAsync(command, timeout);
        }

        public Task<CommandResult> SendAsync(string path, ICommand command, TimeSpan? timeout = null)
        {
            return this.Container.Resolve<ICommandCoordinator>().SendAsync(path, command, timeout);
        }

        public Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null)
        {
            return this.Container.Resolve<ICommandCoordinator>().SendAsync(path, command, timeout);
        }

        public IDomainFacade Domain => this.Container.Resolve<IDomainFacade>();

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

        public Assembly[] Assemblies { get; }


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
        /// Finalizes an instance of the <see cref="AuditStore"/> class.
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
