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
        public Message()
        {
            this.Type = this.GetType();
        }

        /// <inheritdoc />
        public string Id { get; } = NewId.NextId();

        /// <inheritdoc />
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

        /// <inheritdoc />
        public Type Type { get; }
    }
}