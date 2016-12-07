using System;
using System.Linq;
using Slalom.FitStacks.ConsoleClient.Domain;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Configuration;

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
                    Console.WriteLine(container.ResolveAll<IHandleEvent<ItemAddedEvent>>().Count());


                   // container.RegisterModule(new MSSqlServerLoggingModule());

                  //  await container.Bus.Send(new Commands.AddItem.AddItemCommand("tes"));
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