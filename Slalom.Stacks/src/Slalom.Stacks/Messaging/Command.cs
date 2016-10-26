using System;
using System.Linq;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An imperative message to perform an action.  It can either request to change state, which returns an event message, 
    /// or can request data, which returns a document message.
    /// </summary>
    /// <typeparam name="TResult">The type of response expected as a result.</typeparam>
    /// <seealso cref="Slalom.Stacks.Messaging.ICommand" />
    /// <seealso href="http://bit.ly/2d01rc7" />
    public abstract class Command<TResult> : ICommand
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets the message time stamp.
        /// </summary>
        /// <value>The message time stamp.</value>
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        public virtual string CommandName => this.GetType().Name;

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

            return this.Equals((Command<TResult>)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        protected bool Equals(Command<TResult> other)
        {
            return this.Id.Equals(other.Id);
        }
    }
}