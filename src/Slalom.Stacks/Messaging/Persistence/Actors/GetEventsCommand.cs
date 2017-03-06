using System;

namespace Slalom.Stacks.Messaging.Persistence.Actors
{
    public class GetEventsCommand
    {
        public DateTimeOffset? End { get; set; }

        public DateTimeOffset? Start { get; set; }
    }
}