using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Logging;
using Slalom.FitStacks.Runtime;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging
{
    /// <summary>
    /// An application - or in-process - <see cref="IEventPublisher"/> implementation that executes event handlers asynchronously.
    /// </summary>
    /// <seealso cref="Event"/>
    public class EventPublisher : IEventPublisher
    {
        private readonly IComponentContext _componentContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventPublisher"/> class.
        /// </summary>
        /// <param name="componentContext">The configured <see cref="IComponentContext"/> instance.</param>
        /// <param name="logger">The configured <see cref="ILogger"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="componentContext"/> argument is null.</exception> 
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="logger"/> argument is null.</exception> 
        internal EventPublisher(IComponentContext componentContext, ILogger logger)
        {
            Argument.NotNull(() => componentContext);
            Argument.NotNull(() => logger);

            _componentContext = componentContext;
            _logger = logger;
        }

        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="instance">The event to publish.</param>
        /// <param name="context">The current context.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception> 
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception> 
        public async Task Publish<TEvent>(TEvent instance, ExecutionContext context) where TEvent : IEvent
        {
            Argument.NotNull(() => instance);
            Argument.NotNull(() => context);

            var target = (IEnumerable<dynamic>)_componentContext.ResolveAll(typeof(IHandleEvent<>).MakeGenericType(instance.GetType()));

            try
            {
                await Task.WhenAll(target.Select(e => (Task)e.Handle((dynamic)instance, context)));
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Event Handling: {@Event} {@Context}", instance, context);
            }
        }
    }
}