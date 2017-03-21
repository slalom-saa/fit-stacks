﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Reflection
{
    /// <summary>
    /// Scans and locates types and assemblies given the current requestContext.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Reflection.IDiscoverTypes" />
    public class DiscoveryService : IDiscoverTypes
    {
        private static readonly ConcurrentDictionary<Type, List<Type>> Cache = new ConcurrentDictionary<Type, List<Type>>();

        private static readonly string[] Ignores = { "Libuv", "Microsoft.", "NETStandard", "runtime", "xunit" };
        private readonly ILogger _logger;
        private Lazy<List<Assembly>> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryService"/> class.
        /// </summary>
        /// <param name="logger">The configured <see cref="ILogger"/> instance.</param>
        public DiscoveryService(ILogger logger)
        {
            Argument.NotNull(logger, nameof(logger));

            _logger = logger;

            this.CreateAssemblyFactory(logger);
        }

        /// <summary>
        /// Finds available types that are assignable to the specified type.
        /// </summary>
        /// <typeparam name="TType">The type that found types are assignable to.</typeparam>
        /// <returns>All available types that are assignable to the specified type.</returns>
        public IEnumerable<Type> Find<TType>()
        {
            return Cache.GetOrAdd(typeof(TType), t => _assemblies.Value.SafelyGetTypes<TType>().ToList());
        }

        /// <summary>
        /// Finds available types that are assignable to the specified type.
        /// </summary>
        /// <returns>All available types that are assignable to the specified type.</returns>
        public IEnumerable<Type> Find(Type type)
        {
            return Cache.GetOrAdd(type, t => _assemblies.Value.SafelyGetTypes(type).ToList());
        }

        private void CreateAssemblyFactory(ILogger logger)
        {
            _assemblies = new Lazy<List<Assembly>>(() =>
            {
                var assemblies = new List<Assembly>();
#if core
                var dependencies = DependencyContext.Default;
                foreach (var compilationLibrary in dependencies.RuntimeLibraries)
                {
                    try
                    {
                        if (Ignores.Any(e => compilationLibrary.Name.StartsWith(e)))
                        {
                            continue;
                        }

                        var assemblyName = new AssemblyName(compilationLibrary.Name);

                        var assembly = Assembly.Load(assemblyName);

                        assemblies.Add(assembly);
                    }
                    catch
                    {
                        logger?.Debug("DiscoveryService: Could not load library {name}.", compilationLibrary.Name);
                    }
                }
#else
                assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
#endif

                return assemblies;
            });
        }
    }
}