using System;
using System.Linq;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An imperative message to perform an action.  It can either request to change state, which returns an event message, 
    /// or can request data, which returns a document message.
    /// </summary>
    /// <seealso cref="ICommand" />
    /// <seealso href="http://bit.ly/2d01rc7">Reactive Messaging Patterns with the Response Model: Applications and Integration in Scala and Akka</seealso>
    public abstract class Command : ICommand
    {
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        protected Command()
        {
            _type = this.GetType();
        }

        /// <inheritdoc />
        string IMessage.Id { get; } = NewId.NextId();

        /// <inheritdoc />
        DateTimeOffset IMessage.TimeStamp { get; } = DateTimeOffset.Now;

        /// <inheritdoc />
        string ICommand.CommandName => this.GetCommandName();

        /// <inheritdoc />
        Type IMessage.Type => _type;

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

            return this.Equals((Command)obj);
        }

        /// <inheritdoc />
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
            return _type.Name;
        }
    }
}