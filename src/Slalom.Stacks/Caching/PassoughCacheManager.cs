using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Caching
{
    public class PassthroughCacheManager : ICacheManager
    {
        public Task AddAsync<TEntity>(IEnumerable<TEntity> items)
        {
            return Task.FromResult(0);
        }

        public IQueryable<T> OpenQuery<T>(Func<IQueryable<T>> func)
        {
            return func();
        }
    }
}
