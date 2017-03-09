using System;

namespace Slalom.Stacks.Messaging.Logging.EndPoints
{
    public class GetRequestsCommand
    {
        public DateTimeOffset? End { get; set; }

        public DateTimeOffset? Start { get; set; }
    }
}