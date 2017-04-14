namespace Slalom.Stacks.Messaging.EndPoints
{
    [EndPoint("_system/health")]
    public class CheckHealth : EndPoint
    {
        public override void Receive()
        {
        }
    }
}