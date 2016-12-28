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

        public Task<CommandExecuted> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return _commands.Value.SendAsync(command, timeout);
        }

        //public async Task<TResult> SendAsync<TResult>(ICommand command, TimeSpan? timeout = null)
        //{
        //    var result = await _actors.Value.Ask(command, timeout);

        //    return (TResult)result;
        //}

        //public Task<object> SendAsync(ICommand command, TimeSpan? timeout = null)
        //{
        //    return _actors.Value.Ask(command, timeout);
        //}
    }
}