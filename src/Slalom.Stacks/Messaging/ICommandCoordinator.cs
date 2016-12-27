using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public class CommandResult
    {
    }

    public interface ICommandCoordinator
    {
        Task<object> Send(object message, TimeSpan? timeout = null);
    }
}
