using System;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An imperative message to perform an action.
    /// </summary>
    /// <seealso href="http://bit.ly/2d01rc7">Reactive Messaging Patterns with the Response Model: Applications and Integration in Scala and Akka</seealso>
    public interface ICommand : IMessage
    {
        /// <summary>
        /// Gets the command name.
        /// </summary>
        /// <value>The command name.</value>
        string CommandName { get; }
    }
}