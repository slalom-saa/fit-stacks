using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public abstract class Event : IEvent
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

        /// <inheritdoc />
        string IMessage.Id { get; } = NewId.NextId();

        /// <inheritdoc />
        string IEvent.EventName => this.GetEventName();

        /// <inheritdoc />
        int IEvent.EventTypeId => this.GetEventTypeId();

        /// <inheritdoc />
        DateTimeOffset IMessage.TimeStamp { get; } = DateTime.Now;

        private string GetEventName()
        {
            return _attribute.Value?.Name ?? _type.Name;
        }

        private int GetEventTypeId()
        {
            return _attribute.Value?.EventId ?? -1;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return this.Equals((Event)obj);
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return ((IMessage)this).Id.GetHashCode();
        }

        /// <summary>
        /// Determines if another event instance is equal to this instance.
        /// </summary>
        /// <param name="other">The instance to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        protected bool Equals(Event other)
        {
            return ((IMessage)this).Id.Equals(((IMessage)other).Id);
        }

        Type IMessage.Type => _type;
    }
}