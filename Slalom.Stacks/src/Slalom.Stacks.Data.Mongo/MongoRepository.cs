using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Data.Mongo
{
    /// <summary>
    /// A MongoDB <see cref="IRepository{TRoot}"/> implementation.
    /// </summary>
    /// <typeparam name="TEntity">The type that the repository stores and queries.</typeparam>
    /// <seealso cref="Slalom.Stacks.Domain.IRepository{TEntity}" />
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IAggregateRoot
    {
        protected readonly MongoDbContext Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The configured <see cref="MongoDbContext"/>.</param>
        /// <exception>Thrown when the <paramref name="context"/> argument is null.</exception>
        protected MongoRepository(MongoDbContext context)
        {
            Argument.NotNull(() => context);

            Context = context;
        }

        protected Type BaseType => this.GetType().GetTypeInfo().BaseType;

        /// <summary>
        /// Gets or sets the configured <see cref="ILogger"/>.
        /// </summary>
        /// <value>The configured <see cref="ILogger"/>.</value>
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <returns>Task.</returns>
        public virtual Task AddAsync(TEntity[] instances)
        {
            this.Logger.Verbose("Adding {Count} items of type {Type} using {Repository}.", instances.Length, typeof(TEntity).Name, this.BaseType);

            return Context.AddAsync(instances);
        }

        /// <summary>
        /// Clears all instances.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task ClearAsync()
        {
            this.Logger.Verbose("Clearing all items of type {Type} using {Repository}.", typeof(TEntity).Name, this.BaseType);

            return Context.RemoveAsync<TEntity>();
        }

        /// <summary>
        /// Creates an IQueryable that can be used to execute queries.
        /// </summary>
        /// <returns>Returns an IQueryable that can be used to execute queries.</returns>
        public IQueryable<TEntity> CreateQuery()
        {
            this.Logger.Verbose("Finding all items of type {Type} using {Repository}.", typeof(TEntity).Name, this.BaseType);

            return Context.CreateQuery<TEntity>();
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task<TEntity> FindAsync(Guid id)
        {
            this.Logger.Verbose("Finding item of type {Type} with ID {Id} using {Repository}.", typeof(TEntity).Name, id, this.BaseType);

            return Context.FindAsync<TEntity>(id);
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task RemoveAsync(TEntity[] instances)
        {
            this.Logger.Verbose("Removing {Count} items of type {Type} using {Repository}.", instances.Length, typeof(TEntity).Name, this.BaseType);

            return Context.RemoveAsync(instances);
        }

        /// <summary>
        /// Updates the specified instances.
        /// </summary>
        /// <param name="instances">The instances to update.</param>
        /// <returns>Task.</returns>
        public virtual Task UpdateAsync(TEntity[] instances)
        {
            this.Logger.Verbose("Updating {Count} items of type {Type} using {Repository}.", instances.Length, typeof(TEntity).Name, this.BaseType);

            return Context.UpdateAsync(instances);
        }
    }
}