using System;

namespace Slalom.Stacks.Messaging.Persistence.Actors
{
    public class GetRequestsCommand
    {
        public DateTimeOffset? End { get; set; }

        public DateTimeOffset? Start { get; set; }
    }
}