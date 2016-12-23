using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;

namespace Slalom.Stacks.Caching
{
    public class CacheUpdatedMessage : IMessage
    {
        public CacheUpdatedMessage(IEnumerable<Guid> itemsUpdated)
        {
            this.ItemsUpdated = itemsUpdated?.ToArray();
        }

        public IEnumerable<Guid> ItemsUpdated { get; }

        public Guid Id { get; } = Guid.NewGuid();

        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;
    }
}
