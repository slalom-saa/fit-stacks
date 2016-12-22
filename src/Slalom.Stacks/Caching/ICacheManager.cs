using System;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Caching
{
    /// <summary>
    /// Defines a contract for managing a cache.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Adds the items to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task AddAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot;

        /// <summary>
        /// Finds the item in the cache with the specified ID.
        /// </summary>
        /// <typeparam name="TItem">The type of item to find.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task<TItem> FindAsync<TItem>(Guid id) where TItem : IAggregateRoot;

        /// <summary>
        /// Clears all items of the specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of items to clear.</typeparam>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task ClearAsync<TItem>() where TItem : IAggregateRoot;

        /// <summary>
        /// Removes the specified items to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task RemoveAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot;

        /// <summary>
        /// Updates the items in the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of items to update.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task UpdateAsync<TItem>(params TItem[] instances) where TItem : IAggregateRoot;
    }
}