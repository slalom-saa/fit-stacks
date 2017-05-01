using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Logging
{
    public interface IRequestLog
    {
        Task Append(Request request);

        Task<IEnumerable<RequestEntry>> GetEntries(DateTimeOffset? start = null, DateTimeOffset? end = null);
    }
}