using System;
using Akka.Actor;
using Akka.DI.Core;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Communication;

namespace Slalom.Stacks.Actors
{
    public class CommandCoordinationActor : ReceiveActor
    {
        private IActorRef _audit;
        private IActorRef _execution;
        private IActorRef _validator;

        public CommandCoordinationActor()
        {
            this.Receive<ICommand>(e => this.HandleCommandReceived(e));
            this.Receive<ExternalRulesValidatedMessage>(e => this.HandleCommandValidated(e));
            this.Receive<UseCaseExecutionSucceededMessage>(e => this.HandleUseCaseExecuted(e));
            this.Receive<UseCaseExecutionFailedMessage>(e => this.HandleUseCaseExecutionFailed(e));
        }

        private void HandleUseCaseExecutionFailed(UseCaseExecutionFailedMessage message)
        {
            message.Caller.Tell(message.Errors);
        }

        public void HandleCommandReceived(ICommand command)
        {
            _validator.Tell(new ValidateExternalRulesMessage(command, this.Sender), this.Self);
        }

        protected override void PreStart()
        {
            base.PreStart();

            _validator = Context.ActorOf(Context.DI().Props<CommandValidationActor>(), "validation");
            _execution = Context.ActorOf(Context.DI().Props<UseCaseCoordinationActor>(), "execution");
            _audit = Context.ActorOf(Context.DI().Props<AuditActor>(), "audit");
        }

        private void HandleCommandValidated(ExternalRulesValidatedMessage message)
        {
            if (message.ValidationResults.Any())
            {
                message.Caller.Tell(new CommandValidationFailed(message.ValidationResults));

                _audit.Tell(message, this.Self);
            }
            else
            {
                _execution.Tell(new ExecuteUseCaseMessage(message.Command, message.Caller), this.Self);
            }
        }

        private void HandleUseCaseExecuted(UseCaseExecutionSucceededMessage message)
        {
            _audit.Tell(message, this.Self);

            message.Caller.Tell(new CommandExecuted { Result = message.Result }, this.Self);
        }
    }
}