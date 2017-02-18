using System;
using System.Reflection;
using System.Linq;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    public abstract class Message : IMessage
    {
        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        protected Message()
        {
            this.Type = this.GetType();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string IMessage.Id { get; } = NewId.NextId();

        /// <summary>
        /// Gets the message time stamp.
        /// </summary>
        /// <value>The message time stamp.</value>
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.Now;
  
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

            return this.Equals((Message)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return ((IMessage)this).Id.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        protected bool Equals(Message other)
        {
            return ((IMessage)this).Id.Equals(((IMessage)other).Id);
        }
    }
}