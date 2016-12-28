using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IUseCaseCoordinator
    {
        Task<CommandExecuted> SendAsync(ICommand command, TimeSpan? timeout = null);
    }
}
