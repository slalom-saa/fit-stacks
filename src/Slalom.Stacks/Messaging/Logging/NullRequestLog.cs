using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Logging
{
    public class NullRequestLog : IRequestLog
    {
        public Task Append(Request entry)
        {
            return Task.FromResult(0);
        }

        public Task<IEnumerable<RequestEntry>> GetEntries(DateTimeOffset? start, DateTimeOffset? end)
        {
            throw new NotImplementedException();
        }
    }
}
