using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Caching
{
    public class NullCacheConnector : ICacheConnector
    {
        public Task PublishChangesAsync(IEnumerable<string> itemIds)
        {
            return Task.FromResult(0);
        }

        public void OnChanged(Action<CacheUpdatedMessage> action)
        {
        }
    }
}
