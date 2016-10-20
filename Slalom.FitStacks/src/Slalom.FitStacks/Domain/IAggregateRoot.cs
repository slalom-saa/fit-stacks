using System;

namespace Slalom.FitStacks.Domain
{
    /// <summary>
    /// An aggregate root is the root of an associated cluster of objects and are the only objects that
    /// should be loaded from a repository.
    /// </summary>
    /// <seealso href="http://bit.ly/2dVQsXu">Domain-Driven Design: Tackling Complexity in the Heart of Software</seealso>
    public interface IAggregateRoot : IEntity
    {
    }
}