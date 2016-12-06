using System;
using System.Linq;
using System.Threading.Tasks;
using Raven.Abstractions.Commands;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Util;
using Raven.Json.Linq;
using Slalom.Stacks.Domain;
using System.Collections.Generic;

namespace Slalom.Stacks.Data.RavenDB
{
    public class Item : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public static Item Create(int i)
        {
            var target = new Item
            {
                Name = "Item " + i,
            };
            target._names.Add(target.Name);
            return target;
        }

        private List<string> _names = new List<string>();

        public IEnumerable<string> Names => _names.AsEnumerable();
    }

    public class EntityContext : IDisposable
    {
        private IAsyncDocumentSession _session;

        public IAsyncDocumentSession Session => _session ?? (_session = Store.OpenAsyncSession());

        public EntityContext()
        {
            this.Store = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "Local"
            }.Initialize();
        }

        public IDocumentStore Store { get; }

        public void Dispose()
        {
            _session?.Dispose();
        }
    }

    public class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly EntityContext _context;

        public Repository(EntityContext context)
        {
            _context = context;
        }

        public Task ClearAsync()
        {
            return _context.Store.AsyncDatabaseCommands.DeleteByIndexAsync("Raven/DocumentsByEntityName", new IndexQuery
            {
                Query = "Tag:" + Inflector.Pluralize(typeof(T).Name)
            }, new BulkOperationOptions { AllowStale = false });
        }

        public Task RemoveAsync(T[] instances)
        {
            _context.Session.Advanced.Defer(instances.Select(e => new DeleteCommandData { Key = Inflector.Pluralize(typeof(T).Name) + "/" + e.Id }).ToArray());
            return _context.Session.SaveChangesAsync();
        }

        public Task<T> FindAsync(Guid id)
        {
            return _context.Session.LoadAsync<T>(Inflector.Pluralize(typeof(T).Name) + "/" + id);
        }

        public IQueryable<T> CreateQuery()
        {
            return _context.Store.OpenSession().Query<T>();
        }

        public Task AddAsync(T[] instances)
        {
            using (var insert = _context.Store.BulkInsert())
            {
                foreach (var instance in instances)
                {
                    insert.Store(instance);
                }
            }
            return Task.FromResult(0);
        }

        public Task UpdateAsync(T[] instances)
        {
            var commands = instances.Select(e => new PutCommandData { Key = Inflector.Pluralize(typeof(T).Name) + "/" + e.Id, Document = RavenJObject.FromObject(e) });
            _context.Session.Advanced.Defer(commands.ToArray());
            return _context.Session.SaveChangesAsync();
        }
    }
}