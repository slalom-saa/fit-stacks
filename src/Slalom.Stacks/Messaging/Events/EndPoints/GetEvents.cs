using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging.Events.EndPoints
{
    [EndPoint("_systems/events")]
    public class GetEvents : SystemEndPoint<GetEventsCommand, IEnumerable<EventMessage>>
    {
        private readonly IEventStore _events;

        public GetEvents(IEventStore events)
        {
            _events = events;
        }

        public override Task<IEnumerable<EventMessage>> Execute(GetEventsCommand command)
        {
            return _events.GetEvents(command.Start, command.End);
        }
    }
}