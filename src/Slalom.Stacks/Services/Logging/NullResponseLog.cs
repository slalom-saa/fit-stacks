using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Services.Logging
{
    internal class NullResponseLog : IResponseLog
    {
        public Task Append(ResponseEntry entry)
        {
            return Task.FromResult(0);
        }

        public Task<IEnumerable<ResponseEntry>> GetEntries(DateTimeOffset? start, DateTimeOffset? end)
        {
            throw new NotImplementedException();
        }
    }
}