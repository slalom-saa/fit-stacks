namespace Slalom.Stacks.Messaging.Events
{
    public abstract class Event
    {
    }

    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public class EventMessage : Message
    {
        public string RequestId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage" /> class.
        /// </summary>
        /// <param name="body">The message body.</param>
        public EventMessage(string requestId, Event body) : base(body)
        {
            this.RequestId = requestId;
        }
    }
}