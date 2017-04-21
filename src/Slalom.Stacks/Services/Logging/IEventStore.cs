using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Logging
{
    public interface IEventStore
    {
        Task<IEnumerable<EventMessage>> GetEvents(DateTimeOffset? start, DateTimeOffset? end);

        Task Append(EventMessage instance);
    }
}
