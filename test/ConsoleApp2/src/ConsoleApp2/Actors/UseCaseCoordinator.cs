using System;
using Akka.Actor;
using Akka.DI.Core;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Actors
{
    public class UseCaseCoordinator : ReceiveActor
    {
        private readonly IExecutionContextResolver _resolver;
        private IActorRef _audit;
        private IActorRef _execution;
        private IActorRef _validation;

        public UseCaseCoordinator(IExecutionContextResolver resolver)
        {
            _resolver = resolver;

            this.ReceiveAsync<ICommand>(e => this.HandleCommandReceived(e));
            this.Receive<RulesValidated>(e => this.HandleRulesValidated(e));
            this.Receive<UseCaseExecuted>(e => this.HandleUseCaseExecuted(e));
        }

        private void HandleUseCaseExecuted(UseCaseExecuted message)
        {
            message.Result.Complete();

            _audit.Tell(message);

            this.Sender.Tell(message);
        }

        public async Task HandleCommandReceived(ICommand command)
        {
            var context = _resolver.Resolve();

            var message = new ExecuteUseCase(command, context);

            var result = await _validation.Ask<ExecuteUseCase>(message);

            await _execution.Ask(message);

            this.Sender.Tell(result.Result);
        }

        protected override void PreStart()
        {
            base.PreStart();

            _validation = Context.ActorOf(Context.DI().Props<RuleValidator>(), "validation");
            _execution = Context.ActorOf(Context.DI().Props<UseCaseActorCoordinator>(), "execution");
            _audit = Context.ActorOf(Context.DI().Props<Auditor>(), "audit");
        }

        private void HandleRulesValidated(RulesValidated message)
        {
            if (message.Result.ValidationErrors.Any())
            {
                this.HandleUseCaseExecuted(new UseCaseExecuted(message.ExecutionMessage));
            }
            else
            {
                _execution.Tell(message.ExecutionMessage);
            }
        }
    }
}