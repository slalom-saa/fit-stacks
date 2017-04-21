using System;

namespace Slalom.Stacks.Services.EndPoints
{
    public class GetResponsesCommand
    {
        public DateTimeOffset? End { get; set; }

        public DateTimeOffset? Start { get; set; }
    }
}