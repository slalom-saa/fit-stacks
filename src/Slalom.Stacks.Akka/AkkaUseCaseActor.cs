using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    public class AkkaUseCaseActor : ReceiveActor
    {
        public IHandle UseCase { get; }
        public IComponentContext Context { get; }

        public AkkaUseCaseActor(IHandle useCase, IComponentContext context)
        {
            this.UseCase = useCase;
            this.Context = context;

            ReceiveAsync<ICommand>(async e =>
            {
                var result = new CommandResult(new NullExecutionContext());

                await UseCase.HandleAsync(e);

                this.Sender.Tell(result);
            });
        }
    }
}