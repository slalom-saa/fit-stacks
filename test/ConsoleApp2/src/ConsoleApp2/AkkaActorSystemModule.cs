using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.Routing;
using Autofac;
using Slalom.Stacks.Actors;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Reflection;
using Module = Autofac.Module;

namespace Slalom.Stacks
{
    public static class ConfigurationExtensions
    {
        public static ApplicationContainer UseAkka(this ApplicationContainer container)
        {
            container.RegisterModule(new AkkaActorSystemModule(container.Assemblies.ToArray()));

            return container;
        }
    }


    public class AkkaUseCaseCaseCoordinator : IUseCaseCoordinator
    {
        private IActorRef _commands;
        private AutoFacDependencyResolver _resolver;

        public AkkaUseCaseCaseCoordinator(ActorSystem system, ILifetimeScope container)
        {
            _resolver = new AutoFacDependencyResolver(container, system);

            _commands = system.ActorOf(system.DI().Props<UseCaseExecutionCoordinator>().WithRouter(new RoundRobinPool(5)), "commands");
        }

        public Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            var result = _commands.Ask<CommandResult>(command, timeout);

            return result;
        }
    }

    public class AkkaActorSystemModule : Module
    {
        private Assembly[] _assemblies;

        public AkkaActorSystemModule(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => ActorSystem.Create("Patolus"))
                   .AsSelf();

            builder.Register(c => new AkkaUseCaseCaseCoordinator(c.Resolve<ActorSystem>(), c.Resolve<ILifetimeScope>()))
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(ActorBase)))
                   .AsSelf();

            builder.RegisterAssemblyTypes(_assemblies)
                   .Where(e => e.GetBaseAndContractTypes().Any(x => x == typeof(UseCaseActor<,>)))
                   .As(e => e.GetBaseAndContractTypes());
        }
    }
}