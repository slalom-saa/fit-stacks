using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An application - or in-process - <see cref="IEventStream"/> implementation that executes event handlers.
    /// </summary>
    /// <seealso cref="Event"/>
    public class EventStream : IEventStream
    {
        private readonly IComponentContext _componentContext;
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _handers = new ConcurrentDictionary<Type, IEnumerable<object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStream"/> class.
        /// </summary>
        /// <param name="componentContext">The configured <see cref="IComponentContext"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="componentContext"/> argument is null.</exception> 
        internal EventStream(IComponentContext componentContext)
        {
            Argument.NotNull(componentContext, nameof(componentContext));

            _componentContext = componentContext;
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
        public void Publish<TEvent>(TEvent instance, ExecutionContext context) where TEvent : IEvent
        {
            Argument.NotNull(instance, nameof(instance));
            Argument.NotNull(context, nameof(context));

            var target = _handers.GetOrAdd(instance.GetType(), key => _componentContext.ResolveAll(typeof(IHandle<>).MakeGenericType(instance.GetType())));

            foreach (IHandle handler in target)
            {
                handler.SetContext(context);
                handler.HandleAsync(instance).Wait();
            }
        }
    }
}