using System;
using System.Linq;
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
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Returns an IQueryable that can be used to execute queries.</returns>
        IQueryable<TEntity> OpenQuery<TEntity>() where TEntity : IAggregateRoot;

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<TEntity> FindAsync<TEntity>(Guid id) where TEntity : IAggregateRoot;

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