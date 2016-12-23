using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Caching
{
    /// <summary>
    /// A message containing information about what was changed in the cache.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Communication.IMessage" />
    public class CacheUpdatedMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheUpdatedMessage"/> class.
        /// </summary>
        /// <param name="keysUpdated">The keys that were updated.</param>
        public CacheUpdatedMessage(IEnumerable<string> keysUpdated)
        {
            this.KeysUpdated = keysUpdated?.ToArray();
        }

        /// <summary>
        /// Gets the keys updated.
        /// </summary>
        /// <value>The keys updated.</value>
        public IEnumerable<string> KeysUpdated { get; }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public string Id { get; } = NewId.NextId();

        /// <summary>
        /// Gets the message timestamp.
        /// </summary>
        /// <value>The message timestamp.</value>
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;
    }
}