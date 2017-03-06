using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Persistence
{
    public class NullRequestStore : IRequestStore
    {
        public Task Append(RequestEntry entry)
        {
            return Task.FromResult(0);
        }

        public Task<IEnumerable<RequestEntry>> GetEntries(DateTimeOffset? start, DateTimeOffset? end)
        {
            throw new NotImplementedException();
        }
    }
}
