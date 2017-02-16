using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Search;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Configuration;
using Autofac;

namespace Slalom.Stacks.Messaging
{
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
                    container.Container.Update(builder =>
                    {
                        builder.RegisterInstance(new Logger()).AsImplementedInterfaces();
                    });

                    container.UseAkka("local");

                    var tasks = new List<Task>
                    {
                        container.SendAsync("items/add-item", "{}"),
                        container.SendAsync("items/add-item", "{}"),
                        container.SendAsync("items/add-item", "{}"),
                        container.SendAsync("items/add-item", "{}"),
                        container.SendAsync("items/add-item", "{}")
                    };


                    await Task.WhenAll(tasks);

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