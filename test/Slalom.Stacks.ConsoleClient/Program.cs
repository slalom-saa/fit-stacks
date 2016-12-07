using System;
using System.Linq;
using Slalom.FitStacks.ConsoleClient.Commands.AddItem;
using Slalom.FitStacks.ConsoleClient.Domain;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;

namespace Slalom.FitStacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public async void Start()
        {
            try
            {
                using (var container = new ApplicationContainer(typeof(Program)))
                {
                    container.Register<IRepository<Item>>(c => new Repository<Item>(new InMemoryEntityContext()));

                    var result = await container.Bus.Send(new AddItemCommand("testing " + DateTime.Now.Ticks));

                    Console.WriteLine(result.IsSuccessful);
                }

                Console.WriteLine("done");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}