using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Caching
{
    public interface ICacheManager
    {
        Task AddAsync<TEntity>(IEnumerable<TEntity> items);

        IQueryable<T> OpenQuery<T>(Func<IQueryable<T>> func);
    }
}
