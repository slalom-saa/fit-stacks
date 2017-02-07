using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Linq;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
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
    ///     Builds and maintains a runtime by managing dependencies and configuration.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class ApplicationContainer : IDisposable
    {
        private readonly IPropertySelector _selector = new AllUnsetPropertySelector();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationContainer" /> class.
        /// </summary>
        /// <param name="markers">Either a type to be used for assembly scanning, or an instance of the type.</param>
        public ApplicationContainer(params object[] markers)
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

            this.Initialize();
        }

        /// <summary>
        ///     Gets the assemblies that the container uses for services.
        /// </summary>
        /// <value>The assemblies that the container uses for services.</value>
        public Assembly[] Assemblies { get; }

        /// <summary>
        ///     Gets the root <see cref="IContainer" />.
        /// </summary>
        /// <value>The root <see cref="IContainer" />.</value>
        public IContainer Container { get; }

        /// <summary>
        ///     Gets the configured <see cref="IDomainFacade" /> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade" /> instance.</value>
        public IDomainFacade Domain => this.Resolve<IDomainFacade>();

        /// <summary>
        ///     Gets the configured <see cref="ILogger" /> instance.
        /// </summary>
        /// <value>The configured <see cref="ILogger" /> instance.</value>
        public ILogger Logger => this.Resolve<ILogger>();

        /// <summary>
        ///     Gets the configured <see cref="ISearchFacade" /> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade" /> instance.</value>
        public ISearchFacade Search => this.Resolve<ISearchFacade>();

        /// <summary>
        ///     Appends a registered instance with the container, preserving hte default.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="delegate">The instance to register.</param>
        public void Append<T>(Func<IComponentContext, T> @delegate) where T : class
        {
            var builder = new ContainerBuilder();
            builder.Register(c =>
                   {
                       var instance = @delegate.Invoke(c.Resolve<IComponentContext>());

                       this.Container.InjectProperties(instance, _selector);

                       return instance;
                   }).As<T>().AsImplementedInterfaces()
                   .PreserveExistingDefaults();

            builder.Update(this.Container.ComponentRegistry);
        }

        /// <summary>
        ///     Copies this instance to another application container.
        /// </summary>
        /// <returns>Returns the copied application container.</returns>
        public ApplicationContainer Copy()
        {
            var builder = new ContainerBuilder();
            foreach (var registration in this.Container.ComponentRegistry.Registrations)
                builder.RegisterComponent(registration);
            foreach (var source in this.Container.ComponentRegistry.Sources)
                builder.RegisterSource(source);

            var target = new ApplicationContainer();
            builder.Update(target.Container);
            return target;
        }

        /// <summary>
        ///     Populates the container with the set of registered service descriptors
        ///     and makes <see cref="T:System.IServiceProvider" /> and
        ///     <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScopeFactory" />
        ///     available in the container.
        /// </summary>
        /// <param name="services">
        ///     The set of service descriptors to register in the container.
        /// </param>
        public void Populate(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.Update(this.Container.ComponentRegistry);
        }

        /// <summary>
        ///     Registers a component with the container.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        public void Register<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<T>()
                   .AsSelf()
                   .AsImplementedInterfaces();

            builder.Update(this.Container.ComponentRegistry);
        }

        /// <summary>
        ///     Registers an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="delegate">The instance to register.</param>
        public void Register<T>(Func<IComponentContext, T> @delegate) where T : class
        {
            var builder = new ContainerBuilder();
            builder.Register(c =>
            {
                var instance = @delegate.Invoke(c.Resolve<IComponentContext>());

                this.Container.InjectProperties(instance, _selector);

                return instance;
            }).As<T>().AsImplementedInterfaces();

            builder.Update(this.Container.ComponentRegistry);
        }

        /// <summary>
        ///     Registers an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="instance">The instance to register.</param>
        public void Register<T>(T instance) where T : class
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(instance).As<T>().AsImplementedInterfaces();

            builder.Update(this.Container.ComponentRegistry);
        }

        /// <summary>
        ///     Registers an instance with the container using the specified service types.
        /// </summary>
        /// <param name="delegate">The delegate.</param>
        /// <param name="services">The services.</param>
        public void Register(Func<IComponentContext, object> @delegate, params Type[] services)
        {
            var builder = new ContainerBuilder();
            builder.Register(c =>
            {
                var instance = @delegate.Invoke(c.Resolve<IComponentContext>());

                this.Container.InjectProperties(instance, _selector);

                return instance;
            }).As(services);

            builder.Update(this.Container.ComponentRegistry);
        }

        /// <summary>
        ///     Registers the module with the container.
        /// </summary>
        /// <param name="module">The module to register.</param>
        public void RegisterModule(object module)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule((IModule)module);

            builder.Update(this.Container.ComponentRegistry);
        }

        /// <summary>
        ///     Resolves a component from the container.
        /// </summary>
        /// <param name="type">The type of component to resolve.</param>
        /// <returns>The resolved component.</returns>
        public object Resolve(Type type)
        {
            return this.Container.Resolve(type);
        }

        /// <summary>
        ///     Resolves a component from the container.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>T.</returns>
        public T Resolve<T>(Action<T> setup = null)
        {
            T instance;

            if (!this.Container.TryResolve(out instance))
                if (!typeof(T).GetTypeInfo().IsAbstract && !typeof(T).GetTypeInfo().IsInterface)
                {
                    var builder = new ContainerBuilder();

                    builder.RegisterType(typeof(T));

                    builder.Update(this.Container.ComponentRegistry);

                    instance = this.Container.Resolve<T>();
                }

            if (instance != null)
            {
                this.Container.InjectProperties(instance, _selector);

                setup?.Invoke(instance);
            }

            return instance;
        }

        /// <summary>
        ///     Resolves all instance of the specified type from the container.
        /// </summary>
        /// <returns>The resolved instances.</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            var target = this.Container.Resolve<IEnumerable<T>>().ToList();

            foreach (var instance in target)
                this.Container.InjectProperties(instance, _selector);

            return target;
        }

        partial void Initialize();

        #region IDisposable Implementation

        private bool _disposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="ApplicationContainer" /> class.
        /// </summary>
        ~ApplicationContainer()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                this.Container.Dispose();

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion
    }
}