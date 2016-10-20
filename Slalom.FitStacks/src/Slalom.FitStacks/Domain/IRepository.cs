using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.FitStacks.Domain
{
    /// <summary>
    /// Defines a <see href="http://bit.ly/2dVQsXu">Repository</see> for an <see cref="IAggregateRoot"/>.
    /// </summary>
    /// <typeparam name="TRoot">The type of <see cref="IAggregateRoot"/>.</typeparam>
    /// <seealso href="http://bit.ly/2dVQsXu">Domain-Driven Design: Tackling Complexity in the Heart of Software</seealso>
    public interface IRepository<TRoot> where TRoot : IAggregateRoot
    {
        /// <summary>
        /// Removes all instances.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        Task RemoveAsync();

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <param name="instances">The instances to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        Task RemoveAsync(TRoot[] instances);

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task<TRoot> FindAsync(Guid id);

        /// <summary>
        /// Creates an IQueryable that can be used to execute queries.
        /// </summary>
        /// <returns>Returns an IQueryable that can be used to execute queries.</returns>
        IQueryable<TRoot> CreateQuery();

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        Task AddAsync(TRoot[] instances);

        /// <summary>
        /// Updates the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        Task UpdateAsync(TRoot[] instances);
    }
}