using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Represents an Aggregate Root.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Domain.Entity" />
    /// <seealso cref="Slalom.Stacks.Domain.IAggregateRoot" />
    /// <seealso href="https://lostechies.com/jimmybogard/2008/05/21/entities-value-objects-aggregates-and-roots/">Entities, Value Objects, Aggregates and Roots</seealso>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
    }
}