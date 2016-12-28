using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Autofac;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Actors
{
    public class UseCaseActorCoordinator : ReceiveActor
    {
        private readonly IDiscoverTypes _types;
        private readonly IComponentContext _context;

        public UseCaseActorCoordinator(IDiscoverTypes types, IComponentContext context)
        {
            _types = types;
            _context = context;

            this.ReceiveAsync<ExecuteUseCase>(this.HandleExecute);
        }

        public async Task HandleExecute(ExecuteUseCase message)
        {
            //var type = message.Command.GetType();
            //var types = _types.Find(typeof(UseCaseActor<,>));
            //var current = types.FirstOrDefault(e => e.BaseType?.GetGenericArguments().FirstOrDefault() == type);

            //dynamic handler = _context.Resolve(current);

            //object response = await handler.ExecuteAsync((dynamic)message.Command, message.Context);

            this.Sender.Tell(message);
        }
    }
}