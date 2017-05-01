using System;
using Slalom.Stacks.Utilities.NewId;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Messaging
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
            Argument.NotNull(body, nameof(body));

            var type = body.GetType();

            this.Body = body;
            this.MessageType = type.AssemblyQualifiedName;
            this.Name = type.Name;
        }

        /// <inheritdoc />
        public string Id { get; } = NewId.NextId();

        /// <inheritdoc />
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

        /// <inheritdoc />
        public object Body { get; }

        /// <inheritdoc />
        public string MessageType { get; }

        /// <inheritdoc />
        public string Name { get; protected set; }

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