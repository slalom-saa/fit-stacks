using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Caching
{
    public class CacheUpdatedMessage : IMessage
    {
        public CacheUpdatedMessage(IEnumerable<string> itemsUpdated)
        {
            this.ItemsUpdated = itemsUpdated?.ToArray();
        }

        public IEnumerable<string> ItemsUpdated { get; }

        public string Id { get; } = NewId.NextId();

        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;
    }
}
