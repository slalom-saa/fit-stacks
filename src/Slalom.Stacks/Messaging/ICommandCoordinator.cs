using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface ICommandCoordinator
    {
        Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null);

        Task<CommandResult> SendAsync(string path, ICommand command, TimeSpan? timeout = null);

        Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null);
    }
}
