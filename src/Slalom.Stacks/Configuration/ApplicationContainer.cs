using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using System.Linq;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// Builds and maintains a runtime by managing dependencies and configuration.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class ApplicationContainer : IDisposable
    {
        private readonly IContainer _container;
        private readonly IPropertySelector _selector = new AllUnsetPropertySelector();

        bool _disposed;

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

            builder.RegisterModule(new StacksModule { Assemblies = assemblies.ToArray() });

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
        /// Gets the configured <see cref="ILogger"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ILogger"/> instance.</value>
        public ILogger Logger => this.Resolve<ILogger>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/> instance.</value>
        public ISearchFacade Search => this.Resolve<ISearchFacade>();

        public void Configure<T>(string section) where T : class, new()
        {
            var builder = new ContainerBuilder();

            builder.Register(c =>
            {
                var options = Activator.CreateInstance<T>();
                c.Resolve<IConfiguration>().GetSection(section).Bind(options);
                return options;
            });

            builder.Update(_container.ComponentRegistry);
        }

        public void Configure<T>() where T : class, new()
        {
            var builder = new ContainerBuilder();

            builder.Register(c =>
            {
                var options = Activator.CreateInstance<T>();
                c.Resolve<IConfiguration>().Bind(options);
                return options;
            });

            builder.Update(_container.ComponentRegistry);
        }

        /// <summary>
        /// Gets the current <see cref="ExecutionContext"/> instance.
        /// </summary>
        /// <value>The current <see cref="ExecutionContext"/> instance.</value>
        public ExecutionContext GetExecutionContext()
        {
            return this.Resolve<IExecutionContextResolver>()?.Resolve();
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

            builder.Register(c =>
            {
                var instance = @delegate.Invoke(c.Resolve<IComponentContext>());

                _container.InjectProperties(instance, _selector);

                return instance;
            })
                   .As<T>();

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

        /// <summary>
        /// Resolves a component from the container.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>T.</returns>
        public T Resolve<T>(Action<T> setup = null)
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
            var target = _container.Resolve<IEnumerable<T>>();

            foreach (var instance in target)
            {
                _container.InjectProperties(instance, _selector);
            }

            return target.Cast<T>();
        }

        #region Dispose Routine

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
                _container.Dispose();
            }

            _disposed = true;
        }

        #endregion Dispose Routine
    }
}