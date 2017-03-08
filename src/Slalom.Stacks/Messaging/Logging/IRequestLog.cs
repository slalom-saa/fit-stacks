using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Persistence
{
    public interface IRequestLog
    {
        Task Append(Request request);

        Task<IEnumerable<RequestEntry>> GetEntries(DateTimeOffset? start, DateTimeOffset? end);
    }
}