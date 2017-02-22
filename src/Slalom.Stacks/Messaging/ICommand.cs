using System;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An imperative message to perform an action.
    /// </summary>
    /// <seealso href="http://bit.ly/2d01rc7">Reactive Messaging Patterns with the Actor Model: Applications and Integration in Scala and Akka</seealso>
    public interface ICommand : IMessage
    {
        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The message type.</value>
        Type Type { get; }

        /// <summary>
        /// Gets the message name.
        /// </summary>
        /// <value>The message name.</value>
        string CommandName { get; }
    }
}