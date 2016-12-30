using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public interface IUseCaseCoordinator
    {
        Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null);
    }
}
