using System;
using System.Linq;
using Newtonsoft.Json;
using Slalom.FitStacks.ConsoleClient.TestCommands;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging.Serilog;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.ConsoleClient
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
                    container.Register(c => new EntityContext());
                    container.Register(c => new SearchContext());

                    container.RegisterModule(new SerilogModule());

                    await container.Bus.Send(new AddItemCommand("testing"));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}