using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks
{
    public partial class ApplicationContainer
    {
        private Lazy<ICommandCoordinator> _commands;

        partial void Initialize()
        {
            _commands = new Lazy<ICommandCoordinator>(() => this.RootContainer.Resolve<ICommandCoordinator>());
        }

        public ICommandCoordinator Commands => _commands.Value;

    }
}