using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Validation;

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
        /// Gets or sets the configured logger.
        /// </summary>
        /// <value>The configured logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TRoot}" /> class.
        /// </summary>
        /// <param name="context">The configured context.</param>
        public Repository(IEntityContext context)
        {
            Argument.NotNull(context, nameof(context));

            _context = context;
        }

        /// <summary>
        /// Clears all instances.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        public Task ClearAsync()
        {
            this.Logger.Verbose($"Clearing all items of type {typeof(TRoot)} using {_context.GetType()}.");

            return _context.ClearAsync<TRoot>();
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task RemoveAsync(TRoot[] instances)
        {
            Argument.NotNull(instances, nameof(instances));

            this.Logger.Verbose($"Removing {instances.Count()} items of type {typeof(TRoot)} using {_context.GetType()}.");

            return _context.RemoveAsync(instances);
        }

        /// <summary>
        /// Opens a query that can be used to filter and project.
        /// </summary>
        /// <returns>Returns an IQueryable that can be used to execute queries.</returns>
        public IQueryable<TRoot> OpenQuery()
        {
            this.Logger.Verbose($"Opening a query for items of type {typeof(TRoot)} using {_context.GetType()}.");

            return _context.OpenQuery<TRoot>();
        }

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task AddAsync(TRoot[] instances)
        {
            Argument.NotNull(instances, nameof(instances));

            this.Logger.Verbose($"Adding {instances.Count()} items of type {typeof(TRoot)} using {_context.GetType()}.");

            return _context.AddAsync(instances);
        }

        /// <summary>
        /// Updates the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task UpdateAsync(TRoot[] instances)
        {
            Argument.NotNull(instances, nameof(instances));

            this.Logger.Verbose($"Updating {instances.Count()} items of type {typeof(TRoot)} using {_context.GetType()}.");

            return _context.UpdateAsync(instances);
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<TRoot> FindAsync(string id)
        {
            this.Logger.Verbose($"Finding item of type {typeof(TRoot)} with ID {id} using {_context.GetType()}.");

            return _context.FindAsync<TRoot>(id);
        }
    }
}