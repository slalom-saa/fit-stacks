using System;
using System.Collections.Generic;
using Autofac;
using System.Linq;
using System.Reflection;
using Autofac.Core;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Configuration
{
    /// <summary>
    /// Context that contains current components from the container.
    /// </summary>
    /// <seealso cref="Slalom.FitStacks.Configuration.IComponentContext" />
    internal class ComponentContext : IComponentContext
    {
        private readonly Autofac.IComponentContext _context;
        private IPropertySelector _selector = new AllPropertySelector();

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentContext"/> class.
        /// </summary>
        /// <param name="context">The configured <see cref="Autofac.IComponentContext"/> instance.</param>
        /// <exception>Thrown when the <paramref name="context"/> argument is null.</exception>
        internal ComponentContext(Autofac.IComponentContext context)
        {
            Argument.NotNull(() => context);

            _context = context;
        }

        /// <summary>
        /// Resolves an instance of the speciifed type from the context.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The resolved instance.</returns>
        /// <exception>Thrown when the <paramref name="type"/> argument is null.</exception>
        public object Resolve(Type type)
        {
            Argument.NotNull(() => type);

            object instance;

            if (!_context.TryResolve(type, out instance))
            {
                if (!type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterType(type);
                    builder.Update(_context.ComponentRegistry);

                    instance = _context.Resolve(type);
                }
            }

            if (instance != null)
            {
                _context.InjectProperties(instance, _selector);
            }

            return instance;
        }

        /// <summary>
        /// Resolves an instance of the speciifed type from the context.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>The resolved instance.</returns>
        public T Resolve<T>()
        {
            T instance;

            if (!_context.TryResolve<T>(out instance))
            {
                if (!typeof(T).GetTypeInfo().IsAbstract && !typeof(T).GetTypeInfo().IsInterface)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterType(typeof(T));
                    builder.Update(_context.ComponentRegistry);

                    instance = _context.Resolve<T>();
                }
            }

            if (instance != null)
            {
                _context.InjectProperties(instance, _selector);
            }

            return instance;
        }

        /// <summary>
        /// Resolves all instance of the specified type from the container.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The resolved instances.</returns>
        /// <exception>Thrown when the <paramref name="type"/> argument is null.</exception>
        public IEnumerable<object> ResolveAll(Type type)
        {
            Argument.NotNull(() => type);

            var target = (IEnumerable<object>)_context.Resolve(typeof(IEnumerable<>).MakeGenericType(type));

            foreach (var instance in target)
            {
                _context.InjectProperties(instance, _selector);
            }

            return target;
        }
    }
}