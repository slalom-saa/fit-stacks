using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Logging;

namespace Slalom.Stacks.Messaging.Persistence
{
    /// <summary>
    /// Defines a contract for storing logs.  A log is a history of commands that were executed.  Diagnostic logging is done with
    /// the <see cref="ILogger"/>.
    /// </summary>
    public interface IRequestStore
    {
        /// <summary>
        /// Appends an audit with the specified execution elements.
        /// </summary>
        /// <param name="entry">The log entry to append.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Append(RequestEntry entry);

        Task<IEnumerable<RequestEntry>> GetEntries(DateTimeOffset? start, DateTimeOffset? end);
    }
}