using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Logging
{
    public interface IResponseLog
    {
        Task Append(ResponseEntry entry);

        Task<IEnumerable<ResponseEntry>> GetEntries(DateTimeOffset? start, DateTimeOffset? end);
    }
}