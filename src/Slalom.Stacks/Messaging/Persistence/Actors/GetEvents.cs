using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging.Persistence.Actors
{
    [EndPoint("_systems/events")]
    public class GetEvents : EndPoint<GetEventsCommand, IEnumerable<EventMessage>>
    {
        private readonly IEventStore _events;

        public GetEvents(IEventStore events)
        {
            _events = events;
        }

        public override Task<IEnumerable<EventMessage>> ReceiveAsync(GetEventsCommand command)
        {
            return _events.GetEvents(command.Start, command.End);
        }
    }
}