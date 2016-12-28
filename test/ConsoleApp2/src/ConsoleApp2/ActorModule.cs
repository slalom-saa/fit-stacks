using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Slalom.Stacks.Actors;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Reflection;
using CommandExecuted = Slalom.Stacks.Messaging.CommandExecuted;
using Module = Autofac.Module;

namespace Slalom.Stacks
{
    public class AkkaAdapter : IUseCaseCoordinator, IDisposable
    {
        private readonly ActorSystem _system;
        private IActorRef _commands;

        public AkkaAdapter(ActorSystem system, ILifetimeScope container)
        {
            _system = system;

            var props = new AutoFacDependencyResolver(container, _system);

            _commands = system.ActorOf(system.DI().Props<UseCaseCoordinator>(), "commands");
        }

        public void Dispose()
        {
            _system?.Dispose();
        }

        public Task<CommandExecuted> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            var result = _commands.Ask<CommandExecuted>(command, timeout);

            return result;
        }
    }

    public class ActorModule : Module
    {
        private Assembly[] _assemblies;

        public ActorModule(params Type[] markers)
        {
            _assemblies = markers.Select(e => e.Assembly).ToArray();
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new AkkaAdapter(ActorSystem.Create("Patolus"), c.Resolve<ILifetimeScope>()))
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(ActorBase)))
                   .AsSelf();

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(UseCaseActor<,>)))
                   .As(e => e.BaseType);
        }
    }
}