using Slalom.Stacks.Messaging;

namespace Slalom.Products.Application.Shipping.Products.Integration
{
    /// <summary>
    /// Sends a notification to all subscribed listeners.
    /// </summary>
    [Command("notifications/send")]
    public class SendNotificationCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendNotificationCommand" /> class.
        /// </summary>
        /// <param name="name">The name of the notification.</param>
        /// <param name="content">The notification content.</param>
        public SendNotificationCommand(string name, object content)
        {
            this.Name = name;
            this.Content = content;
        }

        /// <summary>
        /// Gets the notification content.
        /// </summary>
        /// <value>The notification content.</value>
        public object Content { get; }

        /// <summary>
        /// Gets the name of the notification.
        /// </summary>
        /// <value>The name of the notification.</value>
        public string Name { get; }
    }
}