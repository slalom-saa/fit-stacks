using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Persistence
{
    /// <summary>
    /// A store for <see cref="ResponseEntry"/> instances.
    /// </summary>
    public interface IResponseStore
    {
        /// <summary>
        /// Appends the specified instance to the store.
        /// </summary>
        /// <param name="entry">The instance to store.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Append(ResponseEntry entry);

        Task<IEnumerable<ResponseEntry>> GetEntries(DateTimeOffset? start, DateTimeOffset? end);
    }
}