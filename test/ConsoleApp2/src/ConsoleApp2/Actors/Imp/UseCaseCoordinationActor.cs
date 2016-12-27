using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Actors
{
    public class UseCaseCoordinationActor : ReceiveActor
    {
        public UseCaseCoordinationActor()
        {
            this.ReceiveAsync<ExecuteUseCaseMessage>(e => this.HandleExecute(e));
            this.ReceiveAsync<ValidateExternalRulesMessage>(e => this.HandleExecute(e));
        }

        public async Task HandleExecute(CommandMessage message)
        {
            var type = message.Command.GetType();
            ICanTell target = Context.GetChildren().FirstOrDefault(e => e.Path.Name == type.Name);
            if (target == null)
            {
                var types = await Context.ActorSelection("/user/discover-types").Ask<IEnumerable<Type>>(typeof(UseCaseActor<,>));
                
                // TODO: find the correct base type
                var current = types.FirstOrDefault(e => e.BaseType?.GetGenericArguments().FirstOrDefault() == type);

                target = Context.ActorOf(Context.DI().Props(current), type.Name);
            }
            target.Tell(new ExecuteUseCaseMessage(message.Command, message.Caller), this.Sender);
        }
    }
}