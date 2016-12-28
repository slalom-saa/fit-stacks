using Akka.Actor;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Actors
{
    public class Auditor : ReceiveActor
    {
        public Auditor()
        {
            this.Receive<UseCaseExecuted>(e =>
            {
            });
        }
    }
}