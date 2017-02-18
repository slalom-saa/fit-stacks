using System;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An atomic packet of data that is transmitted through a message channel.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        string Id { get; }

        /// <summary>
        /// Gets the message timestamp.
        /// </summary>
        /// <value>The message timestamp.</value>
        DateTimeOffset TimeStamp { get; }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The message type.</value>
        Type Type { get; set; }
    }
}