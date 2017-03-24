using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public class NullEventStore : IEventStore
    {
        public Task<IEnumerable<EventMessage>> GetEvents(DateTimeOffset? start, DateTimeOffset? end)
        {
            return Task.FromResult(new EventMessage[0].AsEnumerable());
        }

        public Task Append(EventMessage instance)
        {
            return Task.FromResult(0);
        }
    }
}