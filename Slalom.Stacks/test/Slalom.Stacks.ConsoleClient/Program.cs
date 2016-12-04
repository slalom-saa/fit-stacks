using System;
using System.Linq;
using System.Threading;
using Microsoft.ApplicationInsights;
using Slalom.FitStacks.ConsoleClient.Commands.AddItem;
using Slalom.FitStacks.ConsoleClient.Data;
using Slalom.FitStacks.ConsoleClient.Search;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging.Serilog;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Slalom.Stacks.EntityFramework;
using Slalom.Stacks.Logging.ApplicationInsights;

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
                    container.RegisterModule(new EntityFrameworkLoggingModule("Data Source=localhost;Initial Catalog=Stacks;Integrated Security=True"));
                    container.RegisterModule(new ApplicationInsightsLoggingModule());

                    container.Register(c => new EntityContext());
                    container.Register(c => new SearchContext("Data Source=localhost;Initial Catalog=Stacks;Integrated Security=True"));

                    await container.Resolve<SearchContext>().EnsureMigrations();

                    await container.Bus.Send(new AddItemCommand("testing"));

                    await container.Bus.Send(new AddItemCommand(null));

                    await container.Bus.Send(new AddItemCommand("error"));
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