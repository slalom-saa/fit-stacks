using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Products.Application.Notifications.Send
{
    /// <summary>
    /// Sends a notification to all subscribed listeners.
    /// </summary>
    [EndPoint("notifications/send")]
    public class SendNotification : UseCase<SendNotificationCommand>
    {
        /// <inheritdoc />
        public override void Execute(SendNotificationCommand command)
        {
            Console.WriteLine("Sending notification");

            this.AddRaisedEvent(new SendNotificationEvent(command.Name));
        }
    }
}
