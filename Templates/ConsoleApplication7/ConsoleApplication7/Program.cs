using System;
using ConsoleApplication7.Application.Actors.Products.Add;
using ConsoleApplication7.Application.Actors.Products.Index;
using ConsoleApplication7.Application.Actors.Products.Search;
using Slalom.Stacks;
using Slalom.Stacks.Data.EntityFramework;
using Slalom.Stacks.Data.MongoDb;
using Slalom.Stacks.Logging.ApplicationInsights;
using Slalom.Stacks.Logging.EventHub;
using Slalom.Stacks.Logging.Serilog;
using Slalom.Stacks.Logging.SqlServer;

namespace ConsoleApplication7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Start();
           
        }

        public static async void Start()
        {
            try
            {
                using (var container = new ApplicationContainer(typeof(Program)))
                {
                    //container.UseSqlServerLogging();
                    //container.UseMongoDbRepositories();
                    container.UseApplicationInsightsMetrics();
                    //container.UseSerilogDiagnostics();
                    //container.UseEntityFrameworkSearch();
                    container.UseEventHubLogging(e =>
                    {
                        e.WithConnection(
                                "Endpoint=sb://slalom-stacks.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=F6T4184DOxraeZi72ZBtnqUuNX2P4kLt9xpOmNw8UaA=;")
                            .WithEventHubNames("events", "requests");
                    });

                    await container.Commands.SendAsync(new AddProductCommand("aadfaddssfs", ""));
                    await container.Commands.SendAsync(new IndexProductsCommand());
                    await container.Commands.SendAsync(new SearchProductsCommand("8d8d00006a6a6400af2508d4417fe3f0"));

                    Console.WriteLine("Running application.  Press any key to halt...");
                    Console.ReadKey();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}