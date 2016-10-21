using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slalom.FitStacks.Search
{
    /// <summary>
    /// Provides access to to search results.
    /// </summary>
    public interface ISearchStore<TSearchResult> : IRebuildSearchIndex where TSearchResult : class, ISearchResult
    {
        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <param name="instances">The instances to add.</param>
        Task AddAsync(TSearchResult[] instances);

        /// <summary>
        /// Clears all instances of the specified type.
        /// </summary>
        Task ClearAsync();

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <param name="instances">The instances to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        Task DeleteAsync(TSearchResult[] instances);

        /// <summary>
        /// Removes all instances that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="predicate"/> argument is null.</exception>
        Task DeleteAsync(Expression<Func<TSearchResult, bool>> predicate);

        /// <summary>
        /// Finds all instances of the specified type.
        /// </summary>
        /// <returns>An IQueryable&lt;TSearchResult&gt; that can be used to filter and project.</returns>
        IQueryable<TSearchResult> CreateQuery();

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <param name="id">The instance identifier.</param>
        /// <returns>Returns the instance with the specified identifier.</returns>
        /// <exception cref="System.NotSupportedException">Thrown when an unsupported type is used.</exception>
        Task<TSearchResult> FindAsync(Guid id);

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <param name="instances">The instances to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        Task UpdateAsync(TSearchResult[] instances);

        /// <summary>
        /// Updates all instances found using the specified predicate and uses the provided expression for the update.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="expression">The update make.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="predicate"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="expression"/> argument is null.</exception>
        Task UpdateAsync(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<TSearchResult, TSearchResult>> expression);
    }
}