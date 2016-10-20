using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.FitStacks.Domain
{
    /// <summary>
    /// Provides a single access point to aggregates, allows for repositories to be granular and for
    /// application/infrastructure components to access objects with minimal bloat and lifetime management;  Instead of using
    /// many dependencies, in each class, for each data access component, the facade can be used and it will resolve the
    /// dependences as needed instead of on construction.
    /// </summary>
    public interface IDomainFacade
    {
        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <typeparam name="TAggregateRoot">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task AddAsync<TAggregateRoot>(params TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <typeparam name="TAggregateRoot">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task AddAsync<TAggregateRoot>(IEnumerable<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <typeparam name="TAggregateRoot">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task AddAsync<TAggregateRoot>(List<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Creates a query that can be used to search.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the instance.</typeparam>
        /// <returns>An IQueryable&lt;TAggregateRoot&gt; that can be used to filter and project.</returns>
        IQueryable<TAggregateRoot> CreateQuery<TAggregateRoot>() where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the instance.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Clears all instances of the specified type.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        Task ClearAsync<TAggregateRoot>() where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task RemoveAsync<TAggregateRoot>(params TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task RemoveAsync<TAggregateRoot>(IEnumerable<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task RemoveAsync<TAggregateRoot>(List<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task UpdateAsync<TAggregateRoot>(params TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task UpdateAsync<TAggregateRoot>(IEnumerable<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot;

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <remarks>
        /// This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.
        /// </remarks>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task UpdateAsync<TAggregateRoot>(List<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot;
    }
}