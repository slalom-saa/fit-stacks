using System;
using System.Linq;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Defines information about an Event.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class EventAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventAttribute"/> class.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        public EventAttribute(int eventId = -1)
        {
            this.EventId = eventId;
        }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>The event identifier.</value>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        /// <value>The event name.</value>
        public string Name { get; set; }
    }
}