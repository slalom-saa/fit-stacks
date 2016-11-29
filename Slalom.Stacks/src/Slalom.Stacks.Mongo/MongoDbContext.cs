using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Mongo
{
    public abstract class MongoDbContext
    {
        private readonly string _connection;
        private readonly string _collection;

        protected MongoDbContext()
        {
        }

        protected MongoDbContext(string connection)
        {
            _connection = connection;
        }

        protected MongoDbContext(string connection, string collection)
        {
            _connection = connection;
            _collection = collection;
        }

        protected IConfiguration Configuration { get; set; }

        private IMongoDatabase GetDatabase(string connection)
        {
            var client = !string.IsNullOrWhiteSpace(this.Configuration["Mongo:Connection"]) ? new MongoClient(this.Configuration["Mongo:Connection"])
                             : new MongoClient();

            return client.GetDatabase(connection ?? "local");
        }

        private IMongoCollection<T> GetCollection<T>(string collection, string connection = null)
        {
            return this.GetDatabase(connection).GetCollection<T>(collection);
        }

        private IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return this.GetCollection<TEntity>(_collection ?? typeof(TEntity).Name, _connection);
        }

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : IAggregateRoot
        {
            return this.GetCollection<TEntity>().AsQueryable();
        }

        public Task RemoveAsync<TEntity>() where TEntity : IAggregateRoot
        {
            return this.GetCollection<TEntity>().DeleteManyAsync(e => true);
        }

        public virtual Task RemoveAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            var ids = instances.Select(e => e.Id).ToList();
            return this.GetCollection<TEntity>().DeleteManyAsync(e => ids.Contains(e.Id));
        }

        public virtual async Task<TEntity> FindAsync<TEntity>(Guid id) where TEntity : IAggregateRoot
        {
            var result = await this.GetCollection<TEntity>().FindAsync(e => e.Id == id);

            return result.FirstOrDefault();
        }

        public virtual Task AddAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            return this.GetCollection<TEntity>().InsertManyAsync(instances);
        }

        public virtual Task UpdateAsync<TEntity>(TEntity[] instances) where TEntity : IAggregateRoot
        {
            return Task.WhenAll(
                instances.ToList().Select(e =>
                {
                    return this.GetCollection<TEntity>().ReplaceOneAsync(x => x.Id == e.Id, e, new UpdateOptions { IsUpsert = true });
                }));
        }

    }
}