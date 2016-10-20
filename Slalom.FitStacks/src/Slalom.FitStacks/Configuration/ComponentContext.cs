using System;
using System.Collections.Generic;
using Autofac;
using System.Linq;
using System.Reflection;

namespace Slalom.FitStacks.Configuration
{
    internal class ComponentContext : IComponentContext
    {
        private readonly Autofac.IComponentContext _context;

        internal ComponentContext(Autofac.IComponentContext context)
        {
            _context = context;
        }

        public object Resolve(Type type)
        {
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

            _context.InjectProperties(instance, new AllPropertySelector());

            return instance;
        }

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

            _context.InjectProperties(instance, new AllPropertySelector());

            return instance;
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return (IEnumerable<object>)_context.Resolve(typeof(IEnumerable<>).MakeGenericType(type));
        }
    }
}