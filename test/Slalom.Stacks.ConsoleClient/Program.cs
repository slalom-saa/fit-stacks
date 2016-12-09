using System;
using System.Threading.Tasks;
using Slalom.FitStacks.ConsoleClient.Commands.AddItem;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;

#pragma warning disable 4014

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

        public async Task Start()
        {
            try
            {
                using (var container = new ApplicationContainer(typeof(Program)))
                {
                    container.Register(c => new InMemoryEntityContext());

                    var result = await container.Bus.Send(new AddItemCommand("testing " + DateTime.Now.Ticks));

                    Console.WriteLine(result.IsSuccessful);
                }

                Console.WriteLine("Done with async execution.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}