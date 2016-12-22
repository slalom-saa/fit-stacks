using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Caching
{
    /// <summary>
    /// A local <see cref="ICacheManager"/> implementation that uses an in-memory store.  This is not to be used in a distributed
    /// environment.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Caching.ICacheManager" />
    public class LocalCacheManager : ICacheManager
    {
        private readonly ConcurrentDictionary<Guid, object> _instances = new ConcurrentDictionary<Guid, object>();

        /// <summary>
        /// Adds the items to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task AddAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            foreach (var instance in instances)
            {
                _instances.AddOrUpdate(instance.Id, instance, (id, current) => instance);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds the item in the cache with the specified ID.
        /// </summary>
        /// <typeparam name="TItem">The type of item to find.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task<TItem> FindAsync<TItem>(Guid id) where TItem : IAggregateRoot
        {
            object target;
            if (_instances.TryGetValue(id, out target))
            {
                return Task.FromResult((TItem)target);
            }
            return Task.FromResult(default(TItem));
        }

        /// <summary>
        /// Clears all items of the specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of items to clear.</typeparam>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task ClearAsync<TItem>() where TItem : IAggregateRoot
        {
            var instances = _instances.Where(e => e.Value is TItem);
            foreach (var item in instances)
            {
                object instance = null;
                _instances.TryRemove(item.Key, out instance);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Removes the specified items to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task RemoveAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            var target = _instances.Where(e => instances.Select(x => x.Id).Contains(e.Key));
            foreach (var item in target)
            {
                object instance = null;
                _instances.TryRemove(item.Key, out instance);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Updates the items in the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to update.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task UpdateAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            foreach (var instance in instances)
            {
                _instances.AddOrUpdate(instance.Id, instance, (key, current) => instance);
            }
            return Task.FromResult(0);
        }
    }
}