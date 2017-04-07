namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public class EventMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage" /> class.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="body">The message body.</param>
        public EventMessage(string requestId, Event body) : base(body)
        {
            this.RequestId = requestId;
        }

        /// <summary>
        /// Gets the request message identifier.
        /// </summary>
        /// <value>The request message identifier.</value>
        public string RequestId { get; }
    }
}