using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    public interface ISearchContext
    {
        Task AddAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class;
        Task ClearAsync<TSearchResult>() where TSearchResult : class;
        IQueryable<TSearchResult> CreateQuery<TSearchResult>() where TSearchResult : class;
        Task DeleteAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class;
        Task DeleteAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate) where TSearchResult : class;
        Task<TSearchResult> FindAsync<TSearchResult>(int id) where TSearchResult : class;
        Task UpdateAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class;
        Task UpdateAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<Type, Type>> expression) where TSearchResult : class;
    }
}