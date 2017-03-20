using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Services
{
    [EndPoint("_systems/health")]
    public class CheckHealth : EndPoint
    {
        public override void Receive()
        {
        }
    }
}