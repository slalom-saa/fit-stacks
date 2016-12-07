using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    public class NullSearchContext : ISearchContext
    {
        public Task AddAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        public Task ClearAsync<TSearchResult>() where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        public IQueryable<TSearchResult> CreateQuery<TSearchResult>() where TSearchResult : class
        {
            return Enumerable.Empty<TSearchResult>().AsQueryable();
        }

        public Task DeleteAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        public Task DeleteAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        public Task<TSearchResult> FindAsync<TSearchResult>(int id) where TSearchResult : class
        {
            return Task.FromResult((TSearchResult)null);
        }

        public Task UpdateAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<Type, Type>> expression) where TSearchResult : class
        {
            return Task.FromResult(0);
        }
    }
}
