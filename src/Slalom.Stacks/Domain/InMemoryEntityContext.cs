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

        /// <inheritdoc />
        public Task Add<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
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

        /// <inheritdoc />
        public Task Clear<TEntity>() where TEntity : IAggregateRoot
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

        /// <inheritdoc />
        public async Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IAggregateRoot
        {
            var current = await this.Find(expression);

            return current.Any();
        }

        /// <inheritdoc />
        public Task<TEntity> Find<TEntity>(string id) where TEntity : IAggregateRoot
        {
            CacheLock.EnterReadLock();
            try
            {
                return Task.FromResult((TEntity) Instances.Find(e => e.Id == id));
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : IAggregateRoot
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

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> Find<TEntity>() where TEntity : IAggregateRoot
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

        /// <inheritdoc />
        public Task Remove<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
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

        /// <inheritdoc />
        public async Task Update<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            await this.Remove(instances);

            await this.Add(instances);
        }

        /// <inheritdoc />
        public async Task<bool> Exists<TEntity>(string id) where TEntity : IAggregateRoot
        {
            var target = await this.Find<TEntity>(id);

            return target != null;
        }
    }
}