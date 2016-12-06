using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Domain
{
    public class InMemoryEntityContext : IEntityContext
    {
        private List<IAggregateRoot> _instances = new List<IAggregateRoot>();

        public Task AddAsync<T>(T[] instances) where T : IAggregateRoot
        {
            _instances.AddRange(instances.Cast<IAggregateRoot>());
            return Task.FromResult(0);
        }

        public Task ClearAsync<T>() where T : IAggregateRoot
        {
            _instances.Clear();
            return Task.FromResult(0);
        }

        public IQueryable<T> CreateQuery<T>() where T : IAggregateRoot
        {
            return _instances.OfType<T>().AsQueryable();
        }

        public Task<T> FindAsync<T>(Guid id) where T : IAggregateRoot
        {
            return Task.FromResult(_instances.OfType<T>().FirstOrDefault(e => e.Id == id));
        }

        public Task RemoveAsync<T>(T[] instances) where T : IAggregateRoot
        {
            foreach (var item in instances)
            {
                _instances.Remove(item);
            }
            return Task.FromResult(0);
        }

        public async Task UpdateAsync<T>(T[] instances) where T : IAggregateRoot
        {
            await RemoveAsync(instances);

            await AddAsync(instances);
        }
    }
}
