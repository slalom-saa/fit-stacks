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
        private readonly ICacheConnector _connector;
        private readonly ConcurrentDictionary<Guid, object> _instances = new ConcurrentDictionary<Guid, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalCacheManager"/> class.
        /// </summary>
        /// <param name="connector">The configured <see cref="ICacheConnector"/>.</param>
        public LocalCacheManager(ICacheConnector connector)
        {
            Argument.NotNull(connector, nameof(connector));

            _connector = connector;
        }

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <value>The item count.</value>
        public int ItemCount => _instances.Count;

        /// <summary>
        /// Adds the items to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public virtual async Task AddAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            foreach (var instance in instances)
            {
                _instances.AddOrUpdate(instance.Id, instance, (id, current) => instance);
            }

            await _connector.PublishChangesAsync(instances.Select(e => e.Id));
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task ClearAsync()
        {
            _instances.Clear();

            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds the item in the cache with the specified ID.
        /// </summary>
        /// <typeparam name="TItem">The type of item to find.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public virtual Task<TItem> FindAsync<TItem>(Guid id) where TItem : IAggregateRoot
        {
            object target;
            if (_instances.TryGetValue(id, out target))
            {
                return Task.FromResult((TItem)target);
            }
            return Task.FromResult(default(TItem));
        }

        /// <summary>
        /// Removes the items with the specified keys.
        /// </summary>
        /// <param name="keys">The keys to remove.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public virtual async Task RemoveAsync(params Guid[] keys)
        {
            object instance;
            foreach (var key in keys)
            {
                _instances.TryRemove(key, out instance);
            }

            await _connector.PublishChangesAsync(keys);
        }

        /// <summary>
        /// Removes the specified items to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public virtual async Task RemoveAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            var target = _instances.Where(e => instances.Select(x => x.Id).Contains(e.Key));
            object instance;
            foreach (var item in target)
            {
                _instances.TryRemove(item.Key, out instance);
            }
            await _connector.PublishChangesAsync(instances.Select(e => e.Id));
        }

        /// <summary>
        /// Updates the items in the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to update.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public virtual async Task UpdateAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            foreach (var instance in instances)
            {
                _instances.AddOrUpdate(instance.Id, instance, (key, current) => instance);
            }

            await _connector.PublishChangesAsync(instances.Select(e => e.Id));
        }
    }
}