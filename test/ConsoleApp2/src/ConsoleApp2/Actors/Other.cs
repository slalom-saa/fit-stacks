using Akka.Actor;

namespace Slalom.Stacks.Actors
{
    public class Other : ReceiveActor
    {
        public Other()
        {
            this.Receive<string>(e => this.HandleReceive(e));
        }

        private void HandleReceive(string s)
        {
            this.Sender.Tell("xx");
        }
    }
}