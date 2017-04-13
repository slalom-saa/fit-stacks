using System;

namespace Slalom.Stacks.Messaging.EndPoints
{
    public class GetRequestsCommand
    {
        public DateTimeOffset? End { get; set; }

        public DateTimeOffset? Start { get; set; }
    }
}