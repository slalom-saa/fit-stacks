using System;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging.Logging
{
    /// <summary>
    /// Defines a contract for storing audits.
    /// </summary>
    public interface IAuditStore
    {
        /// <summary>
        /// Appends an audit with the specified execution elements.
        /// </summary>
        /// <param name="audit">The audit entry to append.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task AppendAsync(IEvent @event, ExecutionContext context);
    }
}