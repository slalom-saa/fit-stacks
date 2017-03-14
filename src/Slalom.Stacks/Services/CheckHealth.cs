using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Services
{
    [EndPoint("_systems/health")]
    public class CheckHealth : SystemEndPoint<CheckHealth>
    {
        public override void Execute(CheckHealth instance)
        {
        }
    }
}