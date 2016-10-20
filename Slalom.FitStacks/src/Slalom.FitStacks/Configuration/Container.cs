using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Slalom.FitStacks.Reflection;

namespace Slalom.FitStacks.Configuration
{
    public class AllPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return true;
        }
    }

    /// <summary>
    /// Creates components, wires dependencies and manages lifetime for a set of components.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class Container : IDisposable
    {
        private IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="markers">Either a type to be used for assembly scanning, or an instance of the type.</param>
        public Container(params object[] markers)
        {
            var assemblies = markers.Select(e =>
            {
                if (e is Type)
                {
                    return ((Type)e).GetTypeInfo().Assembly;
                }
                else
                {
                    return e.GetType().GetTypeInfo().Assembly;
                }
            });

            var builder = new ContainerBuilder();

            builder.RegisterModule(new FitStacksModule { Assemblies = assemblies.ToArray() });

            _container = builder.Build();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }

        /// <summary>
        /// Resolves a component from the container.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>T.</returns>
        public T Resolve<T>()
        {
            T instance;

            if (!_container.TryResolve<T>(out instance))
            {
                if (!typeof(T).GetTypeInfo().IsAbstract && !typeof(T).GetTypeInfo().IsInterface)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterType(typeof(T));
                    builder.Update(_container.ComponentRegistry);

                    instance = _container.Resolve<T>();
                }
            }

            _container.InjectProperties(instance, new AllPropertySelector());

            return instance;
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
        /// <param name="instance">The instance to register.</param>
        public void Register<T>(T instance) where T : class
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(instance).As<T>();

            builder.Update(_container.ComponentRegistry);
        }

        public void RegisterModule(object module)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule((IModule)module);

            builder.Update(_container.ComponentRegistry);
        }
    }
}
