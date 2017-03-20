using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Services
{
    [EndPoint("_systems/services")]
    public class GetServices : EndPoint
    {
        private readonly ServiceRegistry _services;

        public GetServices(ServiceRegistry services)
        {
            _services = services;
        }

        public override void Receive()
        {
            this.Respond(_services);
        }
    }
}