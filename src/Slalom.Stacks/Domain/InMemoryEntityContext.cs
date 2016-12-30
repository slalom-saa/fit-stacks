using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Provides an in-memory <see cref="IEntityContext" /> implementation to use with short lived apps and testing.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Domain.IEntityContext" />
    public class InMemoryEntityContext : IEntityContext
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private readonly List<IAggregateRoot> _instances = new List<IAggregateRoot>();

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
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
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
        /// Finds instances with the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression to filter with.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IAggregateRoot
        {
            _cacheLock.EnterReadLock();
            try
            {
                var function = expression.Compile();
                return Task.FromResult(_instances.OfType<TEntity>().Where(function));
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Finds all instances of the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<IEnumerable<TEntity>> FindAsync<TEntity>() where TEntity : IAggregateRoot
        {
            _cacheLock.EnterReadLock();
            try
            {
                return Task.FromResult(_instances.OfType<TEntity>().ToList().AsEnumerable());
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