using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Slalom.Stacks.Caching;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Provides an in-memory <see cref="IEntityContext" /> implementation to use with short lived apps and testing.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Domain.IEntityContext" />
    public class InMemoryEntityContext : IEntityContext
    {
        private List<IAggregateRoot> _instances = new List<IAggregateRoot>();
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task AddAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            _cacheLock.EnterWriteLock();
            try
            {
                foreach (var instance in instances)
                {
                    _instances.Add(instance);
                }
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Clears all instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public Task ClearAsync<TEntity>() where TEntity : IAggregateRoot
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _instances.RemoveAll(e => e is TEntity);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<TEntity> FindAsync<TEntity>(string id) where TEntity : IAggregateRoot
        {
            _cacheLock.EnterReadLock();
            try
            {
                return Task.FromResult((TEntity)_instances.Find(e => e.Id == id));
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Returns an IQueryable that can be used to execute queries.</returns>
        public IQueryable<TEntity> OpenQuery<TEntity>() where TEntity : IAggregateRoot
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _instances.OfType<TEntity>().AsQueryable();
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            _cacheLock.EnterWriteLock();
            try
            {
                var ids = instances.Select(e => e.Id).ToList();
                _instances.RemoveAll(e => ids.Contains(e.Id));
            }
            finally
            {
                _cacheLock.ExitWriteLock();
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