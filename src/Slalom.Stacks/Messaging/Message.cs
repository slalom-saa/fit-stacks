using System;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    ///An atomic packet of data that is transmitted through a message channel.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="body">The message body.</param>
        public Message(object body)
        {
            this.Body = body;
            this.MessageType = body?.GetType();
            this.Name = this.MessageType?.Name;
        }

        public Message(Type type)
        {
            this.MessageType = type;
            this.Name = type.Name;
        }

        /// <inheritdoc />
        public string Id { get; } = NewId.NextId();

        /// <inheritdoc />
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

        /// <inheritdoc />
        public object Body { get; }

        /// <inheritdoc />
        public Type MessageType { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this.Id == (obj as IMessage)?.Id;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>Returns the result of the operator.</returns>
        public static bool operator ==(Message x, Message y)
        {
            return ReferenceEquals(x, y) || x.Equals(y);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>Returns the result of the operator.</returns>
        public static bool operator !=(Message x, Message y)
        {
            return !(x == y);
        }
    }
}