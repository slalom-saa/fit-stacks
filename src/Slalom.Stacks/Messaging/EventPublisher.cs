using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
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
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="componentContext"/> argument is null.</exception> 
        internal EventPublisher(IComponentContext componentContext)
        {
            Argument.NotNull(componentContext, nameof(componentContext));

            _componentContext = componentContext;
            _logger = _componentContext.Resolve<ILogger>();
        }

        private readonly ConcurrentDictionary<Type, IEnumerable> _handers = new ConcurrentDictionary<Type, IEnumerable>();

        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="instance">The event to publish.</param>
        /// <param name="context">The current context.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception> 
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception> 
        public async Task PublishAsync<TEvent>(TEvent instance, ExecutionContext context) where TEvent : IEvent
        {
            Argument.NotNull(instance, nameof(instance));
            Argument.NotNull(context, nameof(context));

            var target = _handers.GetOrAdd(instance.GetType(), (IEnumerable)_componentContext.ResolveAll(typeof(IHandleEvent<>).MakeGenericType(instance.GetType())));

            try
            {
                foreach (dynamic item in target)
                {
                    await (Task)item.Handle((dynamic)instance, context);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "An unhandled exception was raised while handling " + instance.EventName + ": {@Event} {@Context}", instance, context);
            }
        }
    }
}