using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public class AkkaCommandCoordinator : ICommandCoordinator
    {
        private readonly ActorNetwork _network;

        public AkkaCommandCoordinator(ActorNetwork network)
        {
            _network = network;
        }

        public Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return _network.Send(command);
        }

        public Task<CommandResult> SendAsync(string path, ICommand command, TimeSpan? timeout = null)
        {
            return _network.Send(path, command);
        }

        public Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null)
        {
            return _network.Send(path, command);
        }
    }
}