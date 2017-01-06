using System;
using System.Diagnostics.CodeAnalysis;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public abstract class Event : IEvent
    {
        private ExecutionContext _context;

        /// <summary>
        /// Gets the event ID.
        /// </summary>
        /// <value>The event ID.</value>
        string IMessage.Id { get; } = NewId.NextId();

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        string IEvent.EventName => this.GetType().Name;

        ExecutionContext IEvent.Context => _context;

        /// <summary>
        /// Gets the time stamp of when the event was created.
        /// </summary>
        /// <value>The time stamp of when the event was created.</value>
        DateTimeOffset IMessage.TimeStamp { get; } = DateTime.Now;

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

        void IEvent.SetExecutionContext(ExecutionContext context)
        {
            _context = context;
        }
    }
}