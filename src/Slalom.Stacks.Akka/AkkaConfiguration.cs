using Akka.Actor;
using Akka.DI.AutoFac;
using Autofac;

namespace Slalom.Stacks.Messaging
{
    public static class AkkaConfiguration
    {
        public static Stack UseAkka(this Stack instance, string name)
        {
            var system = ActorSystem.Create(name);
            new AutoFacDependencyResolver(instance.Container, system);
            instance.Container.Update(builder =>
            {
                builder.RegisterModule(new AkkaModule(instance.Assemblies));

                builder.Register(c => system).AsSelf().SingleInstance();

                builder.Register(c => new ActorNetwork(system, c.Resolve<IComponentContext>()))
                    .OnActivated(c =>
                    {
                        c.Instance.Arrange(instance.Assemblies);
                    }).SingleInstance().AsSelf().AutoActivate();

                builder.Register(c => new AkkaCommandCoordinator(c.Resolve<ActorNetwork>()))
                    .AsImplementedInterfaces();

            });

            return instance;
        }
    }
}