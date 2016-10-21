using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Domain
{
    /// <summary>
    /// Provides a single access point to aggregates, allows for repositories to be granular and for
    /// application/infrastructure components to access objects with minimal bloat and lifetime management;  Instead of using
    /// many dependencies, in each class, for each data access component, the facade can be used and it will resolve the
    /// dependences as needed instead of on construction.
    /// </summary>
    /// <seealso cref="IDomainFacade" />
    public class DomainFacade : IDomainFacade
    {
        private readonly IComponentContext _componentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainFacade"/> class.
        /// </summary>
        /// <param name="componentContext">The component context.</param>
        public DomainFacade(IComponentContext componentContext)
        {
            Argument.NotNull(() => componentContext);

            _componentContext = componentContext;
        }

        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task AddAsync<TAggregateRoot>(TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot
        {
            if (instances == null)
            {
                throw new ArgumentNullException(nameof(instances));
            }

            if (!instances.Any())
            {
                return Task.FromResult(0);
            }

            var repository = _componentContext.Resolve<IRepository<TAggregateRoot>>();

            return repository.AddAsync(instances);
        }

        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task AddAsync<TAggregateRoot>(IEnumerable<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot
        {
            return this.AddAsync(instances.ToArray());
        }

        /// <summary>
        /// Adds the specified instances. Add is similar to Update, but skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to add.</typeparam>
        /// <param name="instances">The instances to add.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task AddAsync<TAggregateRoot>(List<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot
        {
            return this.AddAsync(instances.ToArray());
        }

        /// <summary>
        /// Creates a query that can be used to search.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the instance.</typeparam>
        /// <returns>An IQueryable&lt;TAggregateRoot&gt; that can be used to filter and project.</returns>
        public IQueryable<TAggregateRoot> CreateQuery<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            return _componentContext.Resolve<IRepository<TAggregateRoot>>().CreateQuery();
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the instance.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id) where TAggregateRoot : IAggregateRoot
        {
            return _componentContext.Resolve<IRepository<TAggregateRoot>>().FindAsync(id);
        }

        /// <summary>
        /// Clears all instances of the specified type.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public Task ClearAsync<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            return _componentContext.Resolve<IRepository<TAggregateRoot>>().RemoveAsync();
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task RemoveAsync<TAggregateRoot>(TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot
        {
            if (instances == null)
            {
                throw new ArgumentNullException(nameof(instances));
            }

            if (!instances.Any())
            {
                return Task.FromResult(0);
            }

            return _componentContext.Resolve<IRepository<TAggregateRoot>>().RemoveAsync(instances);
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task RemoveAsync<TAggregateRoot>(IEnumerable<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot
        {
            return this.RemoveAsync(instances.ToArray());
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task RemoveAsync<TAggregateRoot>(List<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot
        {
            return this.RemoveAsync(instances.ToArray());
        }

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="instances"/> argument is null.</exception>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task UpdateAsync<TAggregateRoot>(TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot
        {
            if (instances == null)
            {
                throw new ArgumentNullException(nameof(instances));
            }

            if (!instances.Any())
            {
                return Task.FromResult(0);
            }

            return _componentContext.Resolve<IRepository<TAggregateRoot>>().UpdateAsync(instances);
        }

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task UpdateAsync<TAggregateRoot>(IEnumerable<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot
        {
            return this.UpdateAsync(instances.ToArray());
        }

        /// <summary>
        /// Updates the specified instances. Update is similar to Add, but Add skips a check to see if the
        /// item already exists.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <param name="instances">The instances to update.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>This allows for performance gain in larger data sets.  If you are unsure
        /// and have a small set, then you can use the update method.</remarks>
        public Task UpdateAsync<TAggregateRoot>(List<TAggregateRoot> instances) where TAggregateRoot : IAggregateRoot
        {
            return this.UpdateAsync(instances.ToArray());
        }
    }
}