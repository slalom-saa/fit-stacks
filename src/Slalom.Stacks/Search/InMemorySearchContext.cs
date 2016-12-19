using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// An in-memory <see cref="ISearchContext" /> instance.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Search.ISearchContext" />
    public class InMemorySearchContext : ISearchContext
    {
        private List<ISearchResult> _instances = new List<ISearchResult>();

        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task AddAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class, ISearchResult
        {
            while (true)
            {
                var original = Interlocked.CompareExchange(ref _instances, null, null);

                var copy = original.ToList();
                copy.AddRange(instances);

                var result = Interlocked.CompareExchange(ref _instances, copy, original);
                if (result == original)
                {
                    break;
                }
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Clears all instances of the specified type.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public Task ClearAsync<TSearchResult>() where TSearchResult : class, ISearchResult
        {
            while (true)
            {
                var original = Interlocked.CompareExchange(ref _instances, null, null);

                var copy = original.ToList();
                copy.Clear();

                var result = Interlocked.CompareExchange(ref _instances, copy, original);
                if (result == original)
                {
                    break;
                }
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of the instance.</typeparam>
        /// <returns>An IQueryable&lt;TAggregateRoot&gt; that can be used to filter and project.</returns>
        public IQueryable<TSearchResult> OpenQuery<TSearchResult>() where TSearchResult : class, ISearchResult
        {
            return _instances.OfType<TSearchResult>().AsQueryable();
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class, ISearchResult
        {
            while (true)
            {
                var original = Interlocked.CompareExchange(ref _instances, null, null);

                var copy = original.ToList();
                foreach (var item in instances)
                {
                    copy.Remove(item);
                }

                var result = Interlocked.CompareExchange(ref _instances, copy, original);
                if (result == original)
                {
                    break;
                }
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Removes the instances that match the specified predicate.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance to remove.</typeparam>
        /// <param name="predicate">The predicate used to filter.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate) where TSearchResult : class, ISearchResult
        {
            while (true)
            {
                var original = Interlocked.CompareExchange(ref _instances, null, null);

                var copy = original.ToList();
                var target = copy.OfType<TSearchResult>().Where(e => predicate.Compile()(e));
                foreach (var item in target)
                {
                    copy.Remove(item);
                }

                var result = Interlocked.CompareExchange(ref _instances, copy, original);
                if (result == original)
                {
                    break;
                }
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of the instance.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<TSearchResult> FindAsync<TSearchResult>(int id) where TSearchResult : class, ISearchResult
        {
            return Task.FromResult(_instances.OfType<TSearchResult>().FirstOrDefault(e => e.Id == id));
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
        public async Task UpdateAsync<TSearchResult>(TSearchResult[] instances) where TSearchResult : class, ISearchResult
        {
            await this.RemoveAsync(instances);

            await this.AddAsync(instances);
        }

        /// <summary>
        /// Updates the specified instances using the specified predicate and update expression.
        /// </summary>
        /// <typeparam name="TSearchResult">The type of instance.</typeparam>
        /// <param name="predicate">The predicate used to filter.</param>
        /// <param name="expression">The expression used to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task UpdateAsync<TSearchResult>(Expression<Func<TSearchResult, bool>> predicate, Expression<Func<Type, Type>> expression) where TSearchResult : class, ISearchResult
        {
            throw new NotSupportedException();
        }
    }
}
