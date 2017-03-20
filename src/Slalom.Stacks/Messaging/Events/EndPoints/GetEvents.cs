using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging.Events.EndPoints
{
    [EndPoint("events", Public = false)]
    public class GetEvents : EndPoint<GetEventsCommand, IEnumerable<EventMessage>>
    {
        private readonly IEventStore _events;

        public GetEvents(IEventStore events)
        {
            _events = events;
        }

        public override Task<IEnumerable<EventMessage>> ReceiveAsync(GetEventsCommand instance)
        {
            return _events.GetEvents(instance.Start, instance.End);
        }
    }
}