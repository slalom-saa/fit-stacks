using Akka.Actor;
using Slalom.Stacks.Communication;

namespace Slalom.Stacks.Actors
{
    public class UseCaseExecutionSucceededMessage : CommandMessage
    {
        public object Result { get; }

        public UseCaseExecutionSucceededMessage(ICommand command, IActorRef caller, object result)
            : base(command, caller)
        {
            this.Result = result;
        }
    }
}