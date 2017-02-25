using System;
using System.Linq;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Contains a method to set the current message execution context.
    /// </summary>
    public interface IUseMessageContext
    {
        /// <summary>
        /// Sets the current message execution context.
        /// </summary>
        /// <param name="context">The context.</param>
        void UseContext(MessageExecutionContext context);
    }
}