using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Persistence
{
    public interface IEventStore
    {
        Task<IEnumerable<Event>> GetEvents(DateTimeOffset? start, DateTimeOffset? end);

        Task Append(Event instance);
    }
}
