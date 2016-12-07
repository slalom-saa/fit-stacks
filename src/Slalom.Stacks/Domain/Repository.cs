using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// A default <see href="http://bit.ly/2dVQsXu">Repository</see> for an <see cref="IAggregateRoot"/>.
    /// </summary>
    /// <typeparam name="TRoot">The type of <see cref="IAggregateRoot"/>.</typeparam>
    /// <seealso href="http://bit.ly/2dVQsXu">Domain-Driven Design: Tackling Complexity in the Heart of Software</seealso>
    public class Repository<TRoot> : IRepository<TRoot> where TRoot : IAggregateRoot
    {
        private readonly IEntityContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TRoot}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(IEntityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Clears all instances.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        public Task ClearAsync()
        {
            return _context.ClearAsync<TRoot>();
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync(TRoot[] instances)
        {
            return _context.RemoveAsync(instances);
        }

        /// <summary>
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <returns>Returns an IQueryable that can be used to execute queries.</returns>
        public IQueryable<TRoot> OpenQuery()
        {
            return _context.CreateQuery<TRoot>();
        }

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <returns>Task.</returns>
        public Task AddAsync(TRoot[] instances)
        {
            return _context.AddAsync(instances);
        }

        /// <summary>
        /// Updates the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <returns>Task.</returns>
        public Task UpdateAsync(TRoot[] instances)
        {
            return _context.UpdateAsync(instances);
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<TRoot> FindAsync(Guid id)
        {
            return _context.FindAsync<TRoot>(id);
        }
    }
}