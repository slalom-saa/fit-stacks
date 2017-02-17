using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

#pragma warning disable 1998

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Provides an in-memory <see cref="IEntityContext" /> implementation to use with short lived apps and testing.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Domain.IEntityContext" />
    public class InMemoryEntityContext : IEntityContext
    {
        /// <summary>
        /// The lock for the instances.
        /// </summary>
        protected readonly ReaderWriterLockSlim CacheLock = new ReaderWriterLockSlim();

        /// <summary>
        /// The in-memory items.
        /// </summary>
        protected readonly List<IAggregateRoot> Instances = new List<IAggregateRoot>();

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task AddAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            Argument.NotNull(instances, nameof(instances));

            CacheLock.EnterWriteLock();
            try
            {
                foreach (var instance in instances)
                {
                    Instances.Add(instance);
                }
            }
            finally
            {
                CacheLock.ExitWriteLock();
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
            CacheLock.EnterWriteLock();
            try
            {
                Instances.RemoveAll(e => e is TEntity);
            }
            finally
            {
                CacheLock.ExitWriteLock();
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
            CacheLock.EnterReadLock();
            try
            {
                return Task.FromResult((TEntity)Instances.Find(e => e.Id == id));
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Finds instances with the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression to filter with.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IAggregateRoot
        {
            CacheLock.EnterReadLock();
            try
            {
                var function = expression.Compile();

                var result = Instances.OfType<TEntity>().Where(function).ToList();

                return result;
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Finds all instances of the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>() where TEntity : IAggregateRoot
        {
            CacheLock.EnterReadLock();
            try
            {
                var result = Instances.OfType<TEntity>().ToList();

                return result;
            }
            finally
            {
                CacheLock.ExitReadLock();
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
            CacheLock.EnterWriteLock();
            try
            {
                var ids = instances.Select(e => e.Id).ToList();
                Instances.RemoveAll(e => ids.Contains(e.Id));
            }
            finally
            {
                CacheLock.ExitWriteLock();
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