using System;
using System.Reflection;
using System.Linq;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An imperative message to perform an action.  It can either request to change state, which returns an event message, 
    /// or can request data, which returns a document message.
    /// </summary>
    /// <seealso cref="ICommand" />
    /// <seealso href="http://bit.ly/2d01rc7">Reactive Messaging Patterns with the Actor Model: Applications and Integration in Scala and Akka</seealso>
    public abstract class Command : ICommand
    {
        private readonly Lazy<RequestAttribute> _attribute;
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        protected Command()
        {
            _type = this.GetType();
            _attribute = new Lazy<RequestAttribute>(() => _type.GetTypeInfo().GetCustomAttributes<RequestAttribute>().FirstOrDefault());
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
        DateTimeOffset IMessage.TimeStamp { get; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the command type.
        /// </summary>
        /// <value>The command type.</value>
        Type ICommand.Type => _type;

        /// <summary>
        /// Gets the command name.
        /// </summary>
        /// <value>The command name.</value>
        string ICommand.CommandName => this.GetCommandName();

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

            return this.Equals((Command)obj);
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
        protected bool Equals(Command other)
        {
            return ((IMessage)this).Id.Equals(((IMessage)other).Id);
        }

        private string GetCommandName()
        {
            return _attribute.Value?.Name ?? _type.Name;
        }
    }
}