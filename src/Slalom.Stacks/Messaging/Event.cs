﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        /// Gets the event ID.
        /// </summary>
        /// <value>The event ID.</value>
        public string Id { get; private set; } = NewId.NextId();

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

        /// <summary>
        /// Gets the time stamp of when the event was created.
        /// </summary>
        /// <value>The time stamp of when the event was created.</value>
        public DateTimeOffset TimeStamp { get; } = DateTime.Now;

        private string GetEventName()
        {
            return _attribute.Value?.Name ?? _type.Name;
        }

        private int GetEventTypeId()
        {
            return _attribute.Value?.EventId ?? -1;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// Determines if another event instance is equal to this instance.
        /// </summary>
        /// <param name="other">The instance to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        protected bool Equals(Event other)
        {
            return other != null && this.Id.Equals(other.Id);
        }

        /// <summary>
        /// Gets the event body payload that will be forwarded.
        /// </summary>
        /// <returns>Returns the event body payload that will be forwarded.</returns>
        public virtual object GetPayload()
        {
            return this;
        }
    }
}