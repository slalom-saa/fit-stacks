using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.AutoFac;
using Autofac;
using Slalom.Stacks.Actors;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Reflection;
using Module = Autofac.Module;

namespace Slalom.Stacks
{
    public class AkkaAdapter : IActorSystem, IDisposable
    {
        private readonly ActorSystem _system;
        private IActorRef _commands;

        public AkkaAdapter(ActorSystem system, ILifetimeScope container)
        {
            _system = system;

            new AutoFacDependencyResolver(container, _system);

            _commands = system.ActorOf<CommandCoordinationActor>("commands");

            system.ActorOf<DiscoverTypesActor>("discover-types");
        }

        public Task<object> Ask(object message, TimeSpan? timeout = null)
        {
            return _commands.Ask(message, timeout);
        }

        public void Tell(object message)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _system?.Dispose();
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