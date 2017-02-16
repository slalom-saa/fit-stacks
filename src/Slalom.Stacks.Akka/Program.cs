using System;
using System.Linq;
using Akka.Actor;
using Akka.DI.AutoFac;
using Slalom.Stacks.Search;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Configuration;

namespace Slalom.Stacks.Messaging
{
    public class AkkaCommandCoordinator : ICommandCoordinator
    {
        private readonly ActorNetwork _network;

        public AkkaCommandCoordinator(ActorNetwork network)
        {
            _network = network;
        }

        public Task<CommandResult> SendAsync(ICommand command, TimeSpan? timeout = null)
        {
            return _network.Send(command);
        }

        public Task<CommandResult> SendAsync(string path, ICommand command, TimeSpan? timeout = null)
        {
            return _network.Send(path, command);
        }

        public Task<CommandResult> SendAsync(string path, string command, TimeSpan? timeout = null)
        {
            return _network.Send(path, command);
        }
    }

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

    public class Program
    {
        public static void Main(string[] args)
        {
            Start();
            Console.WriteLine("Press any key to halt...");
            Console.ReadLine();
        }



        private static async Task Start()
        {
            try
            {
                using (var container = new Stack(typeof(Program)))
                {
                    container.UseAkka("local");

                    var result = await container.SendAsync("items/add-item", "{}");
                    Console.WriteLine(result.IsSuccessful);

                    result = await container.SendAsync(new GoCommand());
                    Console.WriteLine(result.IsSuccessful);

                    //system.ActorOf(system.DI().Props<DefaultActorSupervisor>(), "commands");

                    //var result = await system.ActorSelection("user/items/add-item").Ask(new GoCommand());

                    //Console.WriteLine(result);

                    Console.WriteLine((await container.Domain.FindAsync<Item>()).Count());

                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}