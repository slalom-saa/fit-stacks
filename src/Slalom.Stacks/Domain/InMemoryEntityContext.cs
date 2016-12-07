using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Provides an in-memory <see cref="IEntityContext" /> implementation to use with short lived apps and testing.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Domain.IEntityContext" />
    public class InMemoryEntityContext : IEntityContext
    {
        private readonly List<IAggregateRoot> _instances = new List<IAggregateRoot>();

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task AddAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            _instances.AddRange(instances.Cast<IAggregateRoot>());
            return Task.FromResult(0);
        }

        /// <summary>
        /// Clears all instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public Task ClearAsync<TEntity>() where TEntity : IAggregateRoot
        {
            _instances.Clear();
            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<TEntity> FindAsync<TEntity>(Guid id) where TEntity : IAggregateRoot
        {
            return Task.FromResult(_instances.OfType<TEntity>().FirstOrDefault(e => e.Id == id));
        }

        /// <summary>
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Returns an IQueryable that can be used to execute queries.</returns>
        public IQueryable<TEntity> OpenQuery<TEntity>() where TEntity : IAggregateRoot
        {
            return _instances.OfType<TEntity>().AsQueryable();
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            foreach (var item in instances)
            {
                _instances.Remove(item);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// update as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task UpdateAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            await this.RemoveAsync(instances);

            await this.AddAsync(instances);
        }
    }
}