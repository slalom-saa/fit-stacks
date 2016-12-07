using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Manages a search index with methods to immediately add an item to the index.
    /// </summary>
    /// <typeparam name="TSearchResult">The type of the search result.</typeparam>
    /// <seealso cref="Slalom.Stacks.Search.ISearchIndexer{TSearchResult}" />
    public class SearchIndexer<TSearchResult> : ISearchIndexer<TSearchResult> where TSearchResult : class, ISearchResult
    {
        private readonly ISearchContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchIndexer{TSearchResult}"/> class.
        /// </summary>
        /// <param name="context">The configured context.</param>
        public SearchIndexer(ISearchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds the specified instances to the index immediately. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <param name="instances">The instances to add immediately.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task AddAsync(TSearchResult[] instances)
        {
            return _context.AddAsync(instances);
        }

        /// <summary>
        /// Clears all instances of the specified type.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task ClearAsync()
        {
            return _context.ClearAsync<TSearchResult>();
        }

        /// <summary>
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <returns>An IQueryable&lt;TSearchResult&gt; that can be used to filter and project.</returns>
        public virtual IQueryable<TSearchResult> OpenQuery()
        {
            return _context.OpenQuery<TSearchResult>();
        }

        /// <summary>
        /// Removes all instances that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task DeleteAsync(Expression<Func<TSearchResult, bool>> predicate)
        {
            return _context.RemoveAsync(predicate);
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task DeleteAsync(TSearchResult[] instances)
        {
            return _context.RemoveAsync(instances);
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <param name="id">The instance identifier.</param>
        /// <returns>Returns the instance with the specified identifier.</returns>
        public virtual Task<TSearchResult> FindAsync(int id)
        {
            return _context.FindAsync<TSearchResult>(id);
        }

        /// <summary>
        /// Rebuilds the search index.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task RebuildIndexAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Updates the specified instances immediately. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <param name="instances">The instances to update immediately.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task UpdateAsync(TSearchResult[] instances)
        {
            return _context.UpdateAsync(instances);
        }

        /// <summary>
        /// Updates all instances found using the specified predicate and uses the provided expression for the update.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="expression">The update to make.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual Task UpdateAsync(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<TSearchResult, TSearchResult>> expression)
        {
            throw new NotImplementedException();
        }
    }
}