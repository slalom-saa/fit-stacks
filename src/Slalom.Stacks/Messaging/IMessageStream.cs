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
        Task<MessageResult> Send(ICommand command, MessageContext context = null, TimeSpan? timeout = null);

        Task Publish(IEvent command, MessageContext context = null);

        Task Publish(IEnumerable<IEvent> command, MessageContext context = null);
    }
}
