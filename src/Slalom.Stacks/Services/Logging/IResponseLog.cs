using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Services.Logging
{
    public interface IResponseLog
    {
        Task Append(ResponseEntry entry);

        Task<IEnumerable<ResponseEntry>> GetEntries(DateTimeOffset? start = null, DateTimeOffset? end = null);
    }
}