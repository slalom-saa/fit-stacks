using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Defines an entity context that is used to access a single source.
    /// </summary>
    public interface IEntityContext
    {
        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances" /> argument is null.</exception>
        Task AddAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot;

        /// <summary>
        /// Clears all instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        Task ClearAsync<TEntity>() where TEntity : IAggregateRoot;

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<TEntity> FindAsync<TEntity>(string id) where TEntity : IAggregateRoot;

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression to filter with.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IAggregateRoot;

        /// <summary>
        /// Finds all instances of the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        Task<IEnumerable<TEntity>> FindAsync<TEntity>() where TEntity : IAggregateRoot;

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances" /> argument is null.</exception>
        Task RemoveAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot;

        /// <summary>
        /// Updates the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances" /> argument is null.</exception>
        Task UpdateAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot;
    }
}