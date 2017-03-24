namespace Slalom.Stacks.Messaging
{
    [EndPoint("_systems/health")]
    public class CheckHealth : EndPoint
    {
        public override void Receive()
        {
        }
    }
}