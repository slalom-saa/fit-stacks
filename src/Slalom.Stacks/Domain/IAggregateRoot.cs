using System;
using System.Collections.Generic;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// An aggregate root is the root of an associated cluster of objects and are the only objects that
    /// should be loaded from a repository.
    /// </summary>
    /// <seealso href="http://bit.ly/2dVQsXu">Domain-Driven Design: Tackling Complexity in the Heart of Software</seealso>
    public interface IAggregateRoot : IEntity
    {
        /// <summary>
        /// Gets the raised events.
        /// </summary>
        /// <value>The raised events.</value>
        IEnumerable<Event> Events { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Id { get; }

        /// <summary>
        /// Commits and returns the raised events.
        /// </summary>
        /// <returns>The events that were raised.</returns>
        IEnumerable<Event> CommitEvents();
    }
}