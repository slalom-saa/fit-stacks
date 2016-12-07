using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// The null special case implementation for <see cref="ISearchContext" />.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Search.ISearchContext" />
    public class NullSearchContext : ISearchContext
    {
        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task AddAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Clears all instances of the specified type.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public Task ClearAsync<TSearchResult>() where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of the instance.</typeparam>
        /// <returns>An IQueryable&lt;TAggregateRoot&gt; that can be used to filter and project.</returns>
        public IQueryable<TSearchResult> OpenQuery<TSearchResult>() where TSearchResult : class
        {
            return Enumerable.Empty<TSearchResult>().AsQueryable();
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Removes the instances that match the specified predicate.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance to remove.</typeparam>
        /// <param name="predicate">The predicate used to filter.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of the instance.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<TSearchResult> FindAsync<TSearchResult>(int id) where TSearchResult : class
        {
            return Task.FromResult((TSearchResult)null);
        }

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task UpdateAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Updates the specified instances using the specified predicate and update expression.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance.</typeparam>
        /// <param name="predicate">The predicate used to filter.</param>
        /// <param name="expression">The expression used to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task UpdateAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<Type, Type>> expression) where TSearchResult : class
        {
            return Task.FromResult(0);
        }
    }
}
