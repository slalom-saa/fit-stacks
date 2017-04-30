using System.Linq;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Logging
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
        public EventMessage(string requestId, Event body)
            : base(body)
        {
            this.RequestId = requestId;
            this.Name = this.GetEventName();
        }

        /// <summary>
        /// Gets the request message identifier.
        /// </summary>
        /// <value>The request message identifier.</value>
        public string RequestId { get; }

        private string GetEventName()
        {
            var type = this.GetType();
            var attribute = type.GetAllAttributes<EventAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                return attribute.Name;
            }
            return type.Name;
        }
    }
}