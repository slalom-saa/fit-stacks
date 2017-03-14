using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Services
{

    [EndPoint("_systems/services")]
    public class GetServices : ServiceEndPoint
    {
        private readonly ServiceRegistry _services;

        public GetServices(ServiceRegistry services)
        {
            _services = services;
        }

        public override void Execute()
        {
            this.Respond(_services);
        }
    }
}
