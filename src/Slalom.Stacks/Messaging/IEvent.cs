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

        /// <summary>
        /// Gets the event type identifier used to classify the event.
        /// </summary>
        /// <value>The event type identifier used to classify the event.</value>
        int EventTypeId { get; }
    }
}