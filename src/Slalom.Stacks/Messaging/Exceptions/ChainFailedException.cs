using System;
using System.Linq;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Exceptions
{
    /// <summary>
    /// Exception that should be raised when child execution fails in a command chain.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ChainFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChainFailedException"/> class.
        /// </summary>
        /// <param name="currentMessage">The current message.</param>
        /// <param name="child">The child execution result.</param>
        public ChainFailedException(IMessage currentMessage, MessageResult child)
            : base($"Failed to complete message {currentMessage.Id} because of a failed dependent message {child.RequestId}.", child.RaisedException ?? new ValidationException(child.ValidationErrors.ToArray()))
        {
            this.CurrentMessage = currentMessage;
            this.Child = child;
        }

        /// <summary>
        /// Gets the child execution result.
        /// </summary>
        /// <value>The child execution result.</value>
        public MessageResult Child { get; }

        /// <summary>
        /// Gets the current message.
        /// </summary>
        /// <value>The current message.</value>
        public IMessage CurrentMessage { get; }
    }
}