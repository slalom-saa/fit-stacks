using System;
using System.Linq;
using Slalom.FitStacks.ConsoleClient.Commands.AddItem;
using Slalom.FitStacks.ConsoleClient.Data;
using Slalom.FitStacks.ConsoleClient.Search;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging.Serilog;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.EntityFramework;

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
                    container.RegisterModule(new SerilogModule());
                    container.RegisterModule(new LoggingModule("Data Source=localhost;Initial Catalog=Stacks;Integrated Security=True"));

                    container.Register(c => new EntityContext());
                    container.Register(c => new SearchContext("Data Source=localhost;Initial Catalog=Stacks;Integrated Security=True"));

                    await container.Resolve<SearchContext>().EnsureMigrations();

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