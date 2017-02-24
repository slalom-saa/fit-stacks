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
        Task<MessageResult> Send(ICommand command, MessageExecutionContext context = null, TimeSpan? timeout = null);

        Task<MessageResult> Send(string path, ICommand command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null);

        Task<MessageResult> Send(string path, string command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null);

        Task Publish(IEvent instance, MessageExecutionContext context = null);

        Task Publish(IEnumerable<IEvent> instance, MessageExecutionContext context = null);
    }
}
