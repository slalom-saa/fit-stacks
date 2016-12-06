using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Data
{
    public class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly IEntityContext _context;

        public Repository(IEntityContext context)
        {
            _context = context;
        }

        public Task ClearAsync()
        {
            return _context.ClearAsync<T>();
        }

        public Task RemoveAsync(T[] instances)
        {
            return _context.RemoveAsync(instances);
        }

        public IQueryable<T> CreateQuery()
        {
            return _context.CreateQuery<T>();
        }

        public Task AddAsync(T[] instances)
        {
            return _context.AddAsync<T>(instances);
        }

        public Task UpdateAsync(T[] instances)
        {
            return _context.UpdateAsync(instances);
        }

        public Task<T> FindAsync(Guid id)
        {
            return _context.FindAsync<T>(id);
        }
    }
}