using System;
using System.Linq;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Resolves the message execution context containing information at the current time in processing. This information is otherwise lost
    /// when processing is multi-threaded or distributed.
    /// </summary>
    public interface IExecutionContextResolver
    {
        /// <summary>
        /// Resolves the current execution context.
        /// </summary>
        /// <returns>Returns the current execution context.</returns>
        ExecutionContext Resolve();
    }
}