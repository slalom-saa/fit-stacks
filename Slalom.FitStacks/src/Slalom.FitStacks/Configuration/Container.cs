using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Slalom.FitStacks.Domain;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Reflection;
using Slalom.FitStacks.Search;

namespace Slalom.FitStacks.Configuration
{
    /// <summary>
    /// Creates components, wires dependencies and manages lifetime for a set of components.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class Container : IDisposable
    {
        private IContainer _container;

        bool _disposed;
        private IPropertySelector _selector = new AllPropertySelector();

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="markers">Either a type to be used for assembly scanning, or an instance of the type.</param>
        public Container(params object[] markers)
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

            builder.RegisterModule(new FitStacksModule { Assemblies = assemblies.ToArray() });

            _container = builder.Build();
        }

        /// <summary>
        /// Gets the configured <see cref="IMessageBus"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IMessageBus"/> instance.</value>
        public IMessageBus Bus => this.Resolve<IMessageBus>();

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/> instance.</value>
        public IDomainFacade Domain => this.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/> instance.</value>
        public ISearchFacade Search => this.Resolve<ISearchFacade>();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Container"/> class.
        /// </summary>
        ~Container()
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
                _container.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        /// Resolves a component from the container.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>T.</returns>
        public T Resolve<T>()
        {
            T instance;

            if (!_container.TryResolve(out instance))
            {
                if (!typeof(T).GetTypeInfo().IsAbstract && !typeof(T).GetTypeInfo().IsInterface)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterType(typeof(T));
                    builder.Update(_container.ComponentRegistry);

                    instance = _container.Resolve<T>();
                }
            }

            if (instance != null)
            {
                _container.InjectProperties(instance, _selector);
            }

            return instance;
        }

        /// <summary>
        /// Resolves all instance of the specified type from the container.
        /// </summary>
        /// <returns>The resolved instances.</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            var target = (IEnumerable<object>)_container.Resolve(typeof(IEnumerable<>).MakeGenericType(typeof(T)));

            foreach (var instance in target)
            {
                _container.InjectProperties(instance, _selector);
            }

            return target.Cast<T>();
        }

        /// <summary>
        /// Registers a component with the container.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        public void Register<T>()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<T>()
                   .As(typeof(T).GetBaseAndContractTypes().Where(e => !e.GetTypeInfo().IsGenericTypeDefinition).ToArray());

            builder.Update(_container.ComponentRegistry);
        }

        /// <summary>
        /// Registers an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="delegate">The instance to register.</param>
        public void Register<T>(Func<IComponentContext, T> @delegate) where T : class
        {
            var builder = new ContainerBuilder();

            builder.Register(c => @delegate.Invoke(c.Resolve<IComponentContext>()));

            builder.Update(_container.ComponentRegistry);
        }

        /// <summary>
        /// Registers an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="instance">The instance to register.</param>
        public void Register<T>(T instance) where T : class
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(instance).As<T>();

            builder.Update(_container.ComponentRegistry);
        }

        /// <summary>
        /// Registers the module with the container.
        /// </summary>
        /// <param name="module">The module to register.</param>
        public void RegisterModule(object module)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule((IModule)module);

            builder.Update(_container.ComponentRegistry);
        }
    }
}