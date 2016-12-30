using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks
{
    public partial class ApplicationContainer
    {
        private Lazy<IUseCaseCoordinator> _commands;

        partial void Initialize()
        {
            _commands = new Lazy<IUseCaseCoordinator>(() => this.RootContainer.Resolve<IUseCaseCoordinator>());
        }

        public Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return _commands.Value.SendAsync(command, timeout);
        }
    }
}