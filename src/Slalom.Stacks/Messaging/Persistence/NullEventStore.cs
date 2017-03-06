using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Persistence
{
    public class NullEventStore : IEventStore
    {
        public Task<IEnumerable<Event>> GetEvents(DateTimeOffset? start, DateTimeOffset? end)
        {
            return Task.FromResult(new Event[0].AsEnumerable());
        }

        public Task Append(Event instance)
        {
            return Task.FromResult(0);
        }
    }
}