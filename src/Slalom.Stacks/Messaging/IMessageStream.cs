using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// TBD.
    /// </summary>
    public interface IMessageStream
    {
        Task<MessageResult> Send(ICommand instance, MessageContext context = null, TimeSpan? timeout = null);

        Task Publish(IEvent instance, MessageContext context = null);

        Task Publish(IEnumerable<IEvent> instance, MessageContext context = null);
    }
}
