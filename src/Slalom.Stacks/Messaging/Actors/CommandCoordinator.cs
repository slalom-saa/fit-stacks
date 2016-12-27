using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Configuration;

namespace Slalom.Stacks.Messaging.Actors
{
    public class CommandCoordinator : ICommandCoordinator
    {
        private readonly IComponentContext _context;

        public CommandCoordinator(IComponentContext context)
        {
            _context = context;
        }

        public void Validate()
        {
        }

        public Task<object> Send(object message, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }
    }
}
