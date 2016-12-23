using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Caching
{
    /// <summary>
    /// Defines a contract for a cache connector.  A cache connector is used for messaging between distributed caches.
    /// </summary>
    public interface ICacheConnector
    {
        /// <summary>
        /// Attaches an event handler for when a message is received.
        /// </summary>
        /// <param name="action">The action to perform when a message is received.</param>
        void OnReceived(Action<CacheUpdatedMessage> action);

        /// <summary>
        /// Publishes a message that the items for the specified keys have been modified and should be invalidated.
        /// </summary>
        /// <param name="keys">The keys of the items to invalidate.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task PublishChangesAsync(IEnumerable<string> keys);
    }
}