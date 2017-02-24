using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// TBD.
    /// </summary>
    public interface IMessageDispatcher
    {
        Task<MessageResult> Send(ICommand command, MessageContext context = null, TimeSpan? timeout = null);

        Task<MessageResult> Send(string path, ICommand command, MessageContext parentContext = null, TimeSpan? timeout = null);

        Task<MessageResult> Send(string path, string command, MessageContext parentContext = null, TimeSpan? timeout = null);

        Task Publish(IEvent instance, MessageContext context = null);

        Task Publish(IEnumerable<IEvent> instance, MessageContext context = null);
    }
}
