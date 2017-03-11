using System;

namespace Slalom.Stacks.Messaging.Events.EndPoints
{
    public class GetEventsCommand
    {
        public DateTimeOffset? End { get; set; }

        public DateTimeOffset? Start { get; set; }
    }
}