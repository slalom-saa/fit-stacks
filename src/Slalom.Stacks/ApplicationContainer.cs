using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using IComponentContext = Slalom.Stacks.Configuration.IComponentContext;

#pragma warning disable 618

namespace Slalom.Stacks
{
    /// <summary>
    /// Builds and maintains a runtime by managing dependencies and configuration.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class ApplicationContainer : IDisposable
    {
        private readonly IPropertySelector _selector = new AllUnsetPropertySelector();

        /// <summary>
        /// Resolves a component from the container.
        /// </summary>
        /// <param name="type">The type of component to resolve.</param>
        /// <returns>The resolved component.</returns>
        public object Resolve(Type type)
        {
            return this.RootContainer.Resolve(type);
        }

        partial void Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContainer"/> class.
        /// </summary>
        /// <param name="markers">Either a type to be used for assembly scanning, or an instance of the type.</param>
        public ApplicationContainer(params object[] markers)
        {
            var assemblies = markers.Select(e =>
            {
                var type = e as Type;
                if (type != null)
                {
                    return type.GetTypeInfo().Assembly;
                }
                return e.GetType().GetTypeInfo().Assembly;
            });

            var builder = new ContainerBuilder();

            builder.RegisterModule(new ConfigurationModule { Assemblies = assemblies.ToArray() });

            this.RootContainer = builder.Build();

            this.Initialize();
        }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/> instance.</value>
        public IDomainFacade Domain => this.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ILogger"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ILogger"/> instance.</value>
        public ILogger Logger => this.Resolve<ILogger>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/> instance.</value>
        public ISearchFacade Search => this.Resolve<ISearchFacade>();

        /// <summary>
        /// Gets the root <see cref="IContainer"/>.
        /// </summary>
        /// <value>The root <see cref="IContainer"/>.</value>
        internal IContainer RootContainer { get; }

        /// <summary>
        /// Gets the current <see cref="ExecutionContext"/> instance.
        /// </summary>
        /// <value>The current <see cref="ExecutionContext"/> instance.</value>
        public ExecutionContext GetExecutionContext()
        {
            return this.Resolve<IExecutionContextResolver>()?.Resolve();
        }

        /// <summary>
        /// Populates the container with the set of registered service descriptors
        /// and makes <see cref="T:System.IServiceProvider" /> and <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScopeFactory" />
        /// available in the container.
        /// </summary>
        /// <param name="services">
        /// The set of service descriptors to register in the container.
        /// </param>
        public void Populate(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.Update(this.RootContainer.ComponentRegistry);
        }

        /// <summary>
        /// Registers a component with the container.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        public void Register<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<T>()
                   .AsSelf()
                   .AsImplementedInterfaces();

            builder.Update(this.RootContainer.ComponentRegistry);
        }

        /// <summary>
        /// Registers an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="delegate">The instance to register.</param>
        public void Register<T>(Func<IComponentContext, T> @delegate) where T : class
        {
            var builder = new ContainerBuilder();
            builder.Register(c =>
            {
                var instance = @delegate.Invoke(c.Resolve<IComponentContext>());

                this.RootContainer.InjectProperties(instance, _selector);

                return instance;
            }).As<T>().AsImplementedInterfaces();

            builder.Update(this.RootContainer.ComponentRegistry);
        }

        /// <summary>
        /// Registers an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="instance">The instance to register.</param>
        public void Register<T>(T instance) where T : class
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(instance).As<T>().AsImplementedInterfaces();

            builder.Update(this.RootContainer.ComponentRegistry);
        }

        /// <summary>
        /// Registers an instance with the container using the specified service types.
        /// </summary>
        /// <param name="delegate">The delegate.</param>
        /// <param name="services">The services.</param>
        public void Register(Func<IComponentContext, object> @delegate, params Type[] services)
        {
            var builder = new ContainerBuilder();
            builder.Register(c =>
            {
                var instance = @delegate.Invoke(c.Resolve<IComponentContext>());

                this.RootContainer.InjectProperties(instance, _selector);

                return instance;
            }).As(services);

            builder.Update(this.RootContainer.ComponentRegistry);
        }

        /// <summary>
        /// Registers the module with the container.
        /// </summary>
        /// <param name="module">The module to register.</param>
        public void RegisterModule(object module)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule((IModule)module);

            builder.Update(this.RootContainer.ComponentRegistry);
        }

        /// <summary>
        /// Resolves a component from the container.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>T.</returns>
        public T Resolve<T>(Action<T> setup = null)
        {
            T instance;

            if (!this.RootContainer.TryResolve(out instance))
            {
                if (!typeof(T).GetTypeInfo().IsAbstract && !typeof(T).GetTypeInfo().IsInterface)
                {
                    var builder = new ContainerBuilder();

                    builder.RegisterType(typeof(T));

                    builder.Update(this.RootContainer.ComponentRegistry);

                    instance = this.RootContainer.Resolve<T>();
                }
            }

            if (instance != null)
            {
                this.RootContainer.InjectProperties(instance, _selector);

                setup?.Invoke(instance);
            }

            return instance;
        }

        /// <summary>
        /// Resolves all instance of the specified type from the container.
        /// </summary>
        /// <returns>The resolved instances.</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            var target = this.RootContainer.Resolve<IEnumerable<T>>();

            foreach (var instance in target)
            {
                this.RootContainer.InjectProperties(instance, _selector);
            }

            return target;
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
        /// Finalizes an instance of the <see cref="ApplicationContainer"/> class.
        /// </summary>
        ~ApplicationContainer()
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
                // free other managed objects that implement IDisposable only
                this.RootContainer.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion
    }
}