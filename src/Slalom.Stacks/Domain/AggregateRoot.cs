using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Represents an Aggregate Root.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Domain.Entity" />
    /// <seealso cref="Slalom.Stacks.Domain.IAggregateRoot" />
    /// <seealso href="https://lostechies.com/jimmybogard/2008/05/21/entities-value-objects-aggregates-and-roots/">Entities, Value Objects, Aggregates and Roots</seealso>
    public class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        /// <summary>
        /// Gets the raised events.
        /// </summary>
        /// <value>The raised events.</value>
        public IEnumerable<IEvent> Events => _events.AsEnumerable();

        /// <summary>
        /// Adds the event to the raised events.
        /// </summary>
        /// <param name="event">The event to add.</param>
        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }

        /// <summary>
        /// Commits and returns the raised events.
        /// </summary>
        /// <returns>The events that were raised.</returns>
        public IEnumerable<IEvent> CommitEvents()
        {
            var copy = _events.ToList();
            _events.Clear();
            return copy;
        }
    }
}