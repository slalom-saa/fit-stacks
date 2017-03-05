using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Autofac;
using System.Linq;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
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
        /// Initializes a new instance of the <see cref="Stack" /> class.
        /// </summary>
        /// <param name="markers">Item markers used to identify assemblies.</param>
        public Stack(params object[] markers)
        {
            this.Include(markers);

            var builder = new ContainerBuilder();

            builder.RegisterModule(new ConfigurationModule(this));

            this.Container = builder.Build();
        }

        /// <summary>
        /// Gets the assemblies that are used for loading components.
        /// </summary>
        /// <value>The assemblies that are used for loading components.</value>
        public ObservableCollection<Assembly> Assemblies { get; } = new ObservableCollection<Assembly>();

        /// <summary>
        /// Gets the configured <see cref="IContainer" />.
        /// </summary>
        public IContainer Container { get; }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade" />.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade" />.</value>
        public IDomainFacade Domain => this.Container.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ILogger" />.
        /// </summary>
        /// <value>The configured <see cref="ILogger" />.</value>
        public ILogger Logger => this.Container.Resolve<ILogger>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade" />.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade" />.</value>
        public ISearchFacade Search => this.Container.Resolve<ISearchFacade>();

        public void Include(params object[] markers)
        {
            if (!markers?.Any() ?? true)
            {
                var current = Assembly.GetEntryAssembly();
                var list = new List<Assembly>
                {
                    current
                };
                foreach (var assembly in Directory.GetFiles(Path.GetDirectoryName(current.Location), current.GetName().Name.Split('.')[0] + "*.dll"))
                {
                    list.Add(Assembly.LoadFrom(assembly));
                }
                foreach (var source in list.Distinct())
                {
                    this.Assemblies.Add(source);
                }
            }
            else
            {
                var current = markers.Select(e =>
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
                }).Distinct();
                foreach (var item in current)
                {
                    this.Assemblies.Add(item);
                }
            }
        }

        #region IDisposable Implementation

        private bool _disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Stack" /> class.
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