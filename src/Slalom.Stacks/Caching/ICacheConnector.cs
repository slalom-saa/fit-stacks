using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Caching
{
    public interface ICacheConnector
    {
        Task PublishChangesAsync(IEnumerable<Guid> itemIds);

        void OnChanged(Action<CacheUpdatedMessage> action);
    }
}
