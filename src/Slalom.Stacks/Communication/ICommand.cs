using System;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// An imperative message to perform an action.
    /// </summary>
    /// <seealso href="http://bit.ly/2d01rc7">Reactive Messaging Patterns with the Actor Model: Applications and Integration in Scala and Akka</seealso>
    public interface ICommand : IMessage
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        string CommandName { get; }
    }
}