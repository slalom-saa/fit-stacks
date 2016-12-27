using Akka.Actor;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Actors
{
    public class ValidateExternalRulesMessage : CommandMessage
    {
        public ValidateExternalRulesMessage(ICommand command, IActorRef caller)
            : base(command, caller)
        {
        }
    }

    public class ValidateUseCaseMessage : CommandMessage
    {
        public ValidateUseCaseMessage(ICommand command, IActorRef caller)
            : base(command, caller)
        {
        }
    }
}