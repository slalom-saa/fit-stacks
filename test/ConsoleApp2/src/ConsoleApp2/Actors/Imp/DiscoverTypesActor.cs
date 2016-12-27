using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Akka.Actor;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Actors
{
    public class DiscoverTypesActor : ReceiveActor
    {
        private Lazy<List<Assembly>> _assemblies;
        private ConcurrentDictionary<Type, List<Type>> _cache = new ConcurrentDictionary<Type, List<Type>>();

        public DiscoverTypesActor()
        {
            _assemblies = new Lazy<List<Assembly>>(() =>
            {
                var assemblies = new List<Assembly>();
                assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
                return assemblies;
            });

            this.Receive<Type>(m =>
            {
                this.HandleGetTypes(m);
            });
        }

        public class DiscoverTypesCommand
        {
            public Type Type { get; set; }
        }

        public void HandleGetTypes(Type search)
        {
            var target = _cache.GetOrAdd(search, t => _assemblies.Value.SafelyGetTypes().Where(Predicate).ToList());
            this.Sender.Tell(target);
        }

        private static bool Predicate(Type search)
        {
            if (search.IsGenericTypeDefinition)
            {
                return search.GetBaseAndContractTypes().Any(e => e.IsGenericType && e.GetGenericTypeDefinition() == search);
            }
            else
            {
                return search.IsAssignableFrom(search);
            }
        }
    }
}