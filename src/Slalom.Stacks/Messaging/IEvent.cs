using System;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public interface IEvent : IMessage
    {
        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        string EventName { get; }
    }
}