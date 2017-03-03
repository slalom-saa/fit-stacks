using System;
using System.Reflection;
using System.Linq;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public abstract class Event : IEvent
    {
        private readonly Lazy<EventAttribute> _attribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        protected Event()
        {
            _attribute = new Lazy<EventAttribute>(() => this.GetType().GetCustomAttributes<EventAttribute>().FirstOrDefault());
        }

        /// <inheritdoc />
        public DateTimeOffset TimeStamp { get; } = DateTime.Now;

        /// <inheritdoc />
        public string EventName => this.GetEventName();

        /// <inheritdoc />
        public int EventTypeId => this.GetEventTypeId();

        private string GetEventName()
        {
            return _attribute.Value?.Name ?? this.GetType().Name;
        }

        private int GetEventTypeId()
        {
            return _attribute.Value?.EventId ?? -1;
        }
    }
}