using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Text;
using Service = Slalom.Stacks.Services.Service;

namespace Slalom.Stacks.Configuration.Actors
{
    [EndPoint("_systems/components")]
    public class GetComponents : Service, IHandle<string>
    {
        public Task Handle(string message)
        {
            return Console.Out.WriteLineAsync(message);
        }
    }
}
