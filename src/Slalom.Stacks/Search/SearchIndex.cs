using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    public class SearchIndex<TSearchResult> : ISearchIndex<TSearchResult> where TSearchResult : class, ISearchResult
    {
        private readonly ISearchContext _context;

        public SearchIndex(ISearchContext context)
        {
            _context = context;
        }

        public Task AddAsync(TSearchResult[] instances)
        {
            return _context.AddAsync(instances);
        }

        public Task ClearAsync()
        {
            return _context.ClearAsync<TSearchResult>();
        }

        public IQueryable<TSearchResult> CreateQuery()
        {
            return _context.CreateQuery<TSearchResult>();
        }

        public Task DeleteAsync(Expression<Func<TSearchResult, bool>> predicate)
        {
            return _context.DeleteAsync(predicate);
        }

        public Task DeleteAsync(TSearchResult[] instances)
        {
            return _context.DeleteAsync(instances);
        }

        public Task<TSearchResult> FindAsync(int id)
        {
            return _context.FindAsync<TSearchResult>(id);
        }

        public virtual Task RebuildIndexAsync()
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(TSearchResult[] instances)
        {
            return _context.UpdateAsync(instances);
        }

        public Task UpdateAsync(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<TSearchResult, TSearchResult>> expression)
        {
            throw new NotImplementedException();
        }
    }
}