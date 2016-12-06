using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Data
{
    public interface IEntityContext
    {
        Task AddAsync<T>(T[] instances) where T : IAggregateRoot;
        Task ClearAsync<T>() where T : IAggregateRoot;
        IQueryable<T> CreateQuery<T>() where T : IAggregateRoot;
        Task<T> FindAsync<T>(Guid id) where T : IAggregateRoot;
        Task RemoveAsync<T>(T[] instances) where T : IAggregateRoot;
        Task UpdateAsync<T>(T[] instances) where T : IAggregateRoot;
    }
}