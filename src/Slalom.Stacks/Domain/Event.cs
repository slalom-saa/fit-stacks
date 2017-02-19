using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public abstract class Event : Message, IEvent
    {
        private readonly Lazy<EventAttribute> _attribute;
        private TypeInfo _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        protected Event()
        {
            _type = this.GetType().GetTypeInfo();
            _attribute = new Lazy<EventAttribute>(() => _type.GetCustomAttributes<EventAttribute>().FirstOrDefault());
        }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        string IEvent.EventName => this.GetEventName();

        /// <summary>
        /// Gets the event identifier that is used to classify the event.
        /// </summary>
        /// <value>The event identifier that is used to classify the event.</value>
        int IEvent.EventTypeId => this.GetEventTypeId();

        private string GetEventName()
        {
            return _attribute.Value?.Name ?? _type.Name;
        }

        private int GetEventTypeId()
        {
            return _attribute.Value?.EventId ?? -1;
        }
    }
}