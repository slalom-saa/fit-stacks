using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Slalom.FitStacks.Search;

namespace Slalom.FitStacks.EntityFramework
{
    public abstract class EntityFrameworkSearchIndex<TSearchResult> : ISearchIndex<TSearchResult> where TSearchResult : class, ISearchResult
    {
        private readonly DbContext _context;

        protected EntityFrameworkSearchIndex(DbContext context)
        {
            _context = context;
            this.Set = _context.Set<TSearchResult>();
        }

        protected DbSet<TSearchResult> Set { get; private set; }

        public Task AddAsync(TSearchResult[] instances)
        {
            _context.AddRange(instances);

            return _context.SaveChangesAsync();
        }

        public Task ClearAsync()
        {
            this.Set.RemoveRange(this.Set);

            return _context.SaveChangesAsync();
        }

        public IQueryable<TSearchResult> CreateQuery()
        {
            return this.Set.AsQueryable();
        }

        public Task DeleteAsync(Expression<Func<TSearchResult, bool>> predicate)
        {
            this.Set.RemoveRange(this.Set.Where(predicate));

            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(TSearchResult[] instances)
        {
            this.Set.RemoveRange(instances);

            return _context.SaveChangesAsync();
        }

        public Task<TSearchResult> FindAsync(Guid id)
        {
            return this.Set.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public virtual Task RebuildIndexAsync()
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(TSearchResult[] instances)
        {
            this.Set.UpdateRange(instances);

            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<TSearchResult, TSearchResult>> expression)
        {
            throw new NotImplementedException();
        }
    }
}