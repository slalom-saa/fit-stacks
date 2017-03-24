﻿using System.Collections.Generic;
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
        private readonly List<EventMessage> _events = new List<EventMessage>();

        /// <summary>
        /// Commits and returns the raised events.
        /// </summary>
        /// <returns>The events that were raised.</returns>
        public IEnumerable<EventMessage> CommitEvents()
        {
            var copy = _events.ToList();
            _events.Clear();
            return copy;
        }

        /// <summary>
        /// Adds the eventMessage to the raised events.
        /// </summary>
        /// <param name="eventMessage">The eventMessage to add.</param>
        protected void AddEvent(EventMessage eventMessage)
        {
            _events.Add(eventMessage);
        }
    }
}