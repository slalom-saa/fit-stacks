using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventAttribute : Attribute
    {
        public int EventId { get; }

        public string Name { get; set; }

        public EventAttribute(int eventId = -1)
        {
            this.EventId = eventId;
        }
    }
}
