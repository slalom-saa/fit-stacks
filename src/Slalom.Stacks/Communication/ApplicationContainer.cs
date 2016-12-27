using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;

namespace Slalom.Stacks
{
    public partial class ApplicationContainer
    {
        private Lazy<IActorSystem> _actors;

        partial void Initialize()
        {
            _actors = new Lazy<IActorSystem>(() => this.RootContainer.Resolve<IActorSystem>());
        }

        public async Task<TResult> SendAsync<TResult>(ICommand command, TimeSpan? timeout = null)
        {
            var result = await _actors.Value.Ask(command, timeout);

            return (TResult)result;
        }

        public Task<object> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return _actors.Value.Ask(command, timeout);
        }
    }
}