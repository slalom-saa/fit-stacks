using System;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Caching
{
    /// <summary>
    /// A null <see cref="ICacheManager"/> implementation.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Caching.ICacheManager" />
    public class NullCacheManager : ICacheManager
    {
        /// <summary>
        /// Adds the items to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task AddAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot
        {
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
            return Task.FromResult(default(TItem));
        }

        /// <summary>
        /// Clears all items of the specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of items to clear.</typeparam>
        /// <returns>Returns a task for asynchronous programming.</returns>
        public Task ClearAsync<TItem>() where TItem : IAggregateRoot
        {
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
            return Task.FromResult(0);
        }
    }
}