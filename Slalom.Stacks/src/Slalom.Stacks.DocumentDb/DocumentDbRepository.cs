﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;

namespace Slalom.Stacks.DocumentDb
{
    /// <summary>
    /// A MongoDB <see cref="IRepository{TRoot}"/> implementation.
    /// </summary>
    /// <typeparam name="TEntity">The type that the repository stores and queries.</typeparam>
    /// <seealso cref="Slalom.Stacks.Domain.IRepository{TEntity}" />
    public abstract class DocumentDbRepository<TEntity> : IRepository<TEntity> where TEntity : IAggregateRoot
    {
        private readonly string _collection;
        private readonly string _connection;

        private IMongoCollection<TEntity> _set;

        protected DocumentDbRepository()
            : this(null)
        {
        }

        protected DocumentDbRepository(string connection)
            : this(connection, null)
        {
        }

        protected DocumentDbRepository(string connection, string collection)
        {
            _connection = connection;
            _collection = collection ?? typeof(TEntity).Name;
        }

        protected DocumentDbConnectionManager ConnectionManager { get; set; }

        public ILogger Logger { get; set; }

        protected IMongoCollection<TEntity> Set => _set ?? (_set = this.ConnectionManager.GetCollection<TEntity>(collection: _collection, connection: _connection));

        public virtual Task RemoveAsync()
        {
            return this.Set.DeleteManyAsync(e => true);
        }

        public virtual Task RemoveAsync(TEntity[] instances)
        {
            var ids = instances.Select(e => e.Id).ToList();
            return this.Set.DeleteManyAsync(e => ids.Contains(e.Id));
        }

        public virtual async Task<TEntity> FindAsync(Guid id)
        {
            var result = await this.Set.FindAsync(e => e.Id == id);

            return result.FirstOrDefault();
        }

        public virtual IQueryable<TEntity> CreateQuery()
        {
            return this.Set.AsQueryable();
        }

        public virtual Task AddAsync(TEntity[] instances)
        {
            return this.Set.InsertManyAsync(instances);
        }

        public virtual Task UpdateAsync(TEntity[] instances)
        {
            return Task.WhenAll(
                instances.ToList().Select(e =>
                {
                    return this.Set.ReplaceOneAsync(x => x.Id == e.Id, e, new UpdateOptions { IsUpsert = true });
                }));
        }
    }
}