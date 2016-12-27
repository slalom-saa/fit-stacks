using Akka.Actor;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Actors
{
    public class AuditActor : ReceiveActor
    {
        public AuditActor()
        {
            this.Receive<ExternalRulesValidatedMessage>(e =>
            {
                //Console.WriteLine("Audit fail.");
            });

            this.Receive<UseCaseExecutionSucceededMessage>(e =>
            {
                if (e.Result is IEvent)
                {
                    Context.System.EventStream.Publish(e);
                }
            });

            this.Receive<UseCaseExecutionFailedMessage>(e =>
            {
            });
        }
    }
}