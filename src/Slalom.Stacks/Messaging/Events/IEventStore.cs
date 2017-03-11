using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Events
{
    public interface IEventStore
    {
        Task<IEnumerable<EventMessage>> GetEvents(DateTimeOffset? start, DateTimeOffset? end);

        Task Append(EventMessage instance);
    }
}
