using System;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging.Logging
{
    /// <summary>
    /// Defines a contract for storing events.
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Appends an event with the specified execution elements.
        /// </summary>
        /// <param name="entry">The event entry to append.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task AppendAsync(EventEntry entry);
    }
}