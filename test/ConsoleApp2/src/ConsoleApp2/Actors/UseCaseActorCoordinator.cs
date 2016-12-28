using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Actors
{
    public class UseCaseActorCoordinator : ReceiveActor
    {
        private readonly IDiscoverTypes _types;

        public UseCaseActorCoordinator(IDiscoverTypes types)
        {
            _types = types;
            this.Receive<ExecuteUseCase>(e => this.HandleExecute(e));
        }

        public void HandleExecute(ExecuteUseCase message)
        {
            var type = message.Command.GetType();
            ICanTell target = Context.GetChildren().FirstOrDefault(e => e.Path.Name == type.Name);
            if (target == null)
            {
                var types = _types.Find(typeof(UseCaseActor<,>));
                var current = types.FirstOrDefault(e => e.BaseType?.GetGenericArguments().FirstOrDefault() == type);

                target = Context.ActorOf(Context.DI().Props(current), type.Name);
            }
            target.Tell(message, this.Sender);
        }
    }
}