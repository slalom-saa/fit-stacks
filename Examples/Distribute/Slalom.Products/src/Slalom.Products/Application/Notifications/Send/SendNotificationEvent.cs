using Slalom.Stacks.Messaging.Events;

namespace Slalom.Products.Application.Notifications.Send
{
    /// <summary>
    /// Raised when a notification is sent.
    /// </summary>
    public class SendNotificationEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendNotificationEvent" /> class.
        /// </summary>
        /// <param name="name">The name of the notification.</param>
        public SendNotificationEvent(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name of the notification.</value>
        public string Name { get; }
    }
}