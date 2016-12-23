using System;
using System.Linq;

namespace Slalom.Stacks.Communication
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
        Guid Id { get; }

        /// <summary>
        /// Gets the message timestamp.
        /// </summary>
        /// <value>The message timestamp.</value>
        DateTimeOffset TimeStamp { get; }
    }
}