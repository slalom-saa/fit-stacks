using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Caching;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Domain
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
        private readonly ICacheManager _cacheManager;
        private readonly IComponentContext _componentContext;
        private readonly ConcurrentDictionary<Type, object> _instances = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainFacade" /> class.
        /// </summary>
        /// <param name="componentContext">The component context.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public DomainFacade(IComponentContext componentContext, ICacheManager cacheManager)
        {
            Argument.NotNull(componentContext, nameof(componentContext));
            Argument.NotNull(cacheManager, nameof(cacheManager));

            _componentContext = componentContext;
            _cacheManager = cacheManager;
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
        public async Task AddAsync<TAggregateRoot>(TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot
        {
            if (instances == null)
            {
                throw new ArgumentNullException(nameof(instances));
            }

            if (!instances.Any())
            {
                return;
            }

            var repository = (IRepository<TAggregateRoot>)_instances.GetOrAdd(typeof(TAggregateRoot), t => _componentContext.Resolve<IRepository<TAggregateRoot>>());

            if (repository == null)
            {
                throw new InvalidOperationException($"No repository has been registered for type {typeof(TAggregateRoot)}.");
            }

            await repository.AddAsync(instances);

            await _cacheManager.AddAsync(instances);
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
        /// Clears all instances of the specified type.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task ClearAsync<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            var repository = (IRepository<TAggregateRoot>)_instances.GetOrAdd(typeof(TAggregateRoot), t => _componentContext.Resolve<IRepository<TAggregateRoot>>());

            if (repository == null)
            {
                throw new InvalidOperationException($"No repository has been registered for type {typeof(TAggregateRoot)}.");
            }

            await repository.ClearAsync();

            await _cacheManager.ClearAsync();
        }

        /// <summary>
        /// Finds the instance with the specified identifier.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the instance.</typeparam>
        /// <param name="id">The instance identifier.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(string id) where TAggregateRoot : IAggregateRoot
        {
            var target = await _cacheManager.FindAsync<TAggregateRoot>(id);
            if (target != null)
            {
                return target;
            }

            var repository = (IRepository<TAggregateRoot>)_instances.GetOrAdd(typeof(TAggregateRoot), t => _componentContext.Resolve<IRepository<TAggregateRoot>>());

            if (repository == null)
            {
                throw new InvalidOperationException($"No repository has been registered for type {typeof(TAggregateRoot)}.");
            }

            target = await repository.FindAsync(id);

            if (target != null)
            {
                await _cacheManager.AddAsync(target);
            }

            return target;
        }

        /// <summary>
        /// Finds instances with the specified expression.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the instance.</typeparam>
        /// <param name="expression">The expression to filter with.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task<IEnumerable<TAggregateRoot>> FindAsync<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> expression) where TAggregateRoot : IAggregateRoot
        {
            var repository = (IRepository<TAggregateRoot>)_instances.GetOrAdd(typeof(TAggregateRoot), t => _componentContext.Resolve<IRepository<TAggregateRoot>>());

            if (repository == null)
            {
                throw new InvalidOperationException($"No repository has been registered for type {typeof(TAggregateRoot)}.");
            }

            return repository.FindAsync(expression);
        }

        /// <summary>
        /// Finds all instances of the specified type.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the instance.</typeparam>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<IEnumerable<TAggregateRoot>> FindAsync<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            var repository = (IRepository<TAggregateRoot>)_instances.GetOrAdd(typeof(TAggregateRoot), t => _componentContext.Resolve<IRepository<TAggregateRoot>>());

            if (repository == null)
            {
                throw new InvalidOperationException($"No repository has been registered for type {typeof(TAggregateRoot)}.");
            }

            return repository.FindAsync();
        }

        /// <summary>
        /// Removes the specified instances.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of instance to remove.</typeparam>
        /// <param name="instances">The instances to remove.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task RemoveAsync<TAggregateRoot>(TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot
        {
            if (instances == null)
            {
                throw new ArgumentNullException(nameof(instances));
            }

            if (!instances.Any())
            {
                return;
            }

            var repository = (IRepository<TAggregateRoot>)_instances.GetOrAdd(typeof(TAggregateRoot), t => _componentContext.Resolve<IRepository<TAggregateRoot>>());

            if (repository == null)
            {
                throw new InvalidOperationException($"No repository has been registered for type {typeof(TAggregateRoot)}.");
            }

            await repository.RemoveAsync(instances);

            await _cacheManager.RemoveAsync(instances);
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
        public async Task UpdateAsync<TAggregateRoot>(TAggregateRoot[] instances) where TAggregateRoot : IAggregateRoot
        {
            if (instances == null)
            {
                throw new ArgumentNullException(nameof(instances));
            }

            if (!instances.Any())
            {
                return;
            }

            var repository = (IRepository<TAggregateRoot>)_instances.GetOrAdd(typeof(TAggregateRoot), t => _componentContext.Resolve<IRepository<TAggregateRoot>>());

            if (repository == null)
            {
                throw new InvalidOperationException($"No repository has been registered for type {typeof(TAggregateRoot)}.");
            }

            await repository.UpdateAsync(instances);

            await _cacheManager.UpdateAsync(instances);
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