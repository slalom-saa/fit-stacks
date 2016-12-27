using Akka.Actor;

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
                //Console.WriteLine("Audit success.");
            });
        }
    }
}