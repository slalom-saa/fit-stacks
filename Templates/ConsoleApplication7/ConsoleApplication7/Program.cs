using System;
using System.Linq;
using ConsoleApplication7.Application.Actors.Products.Add;
using ConsoleApplication7.Application.Actors.Products.Index;
using ConsoleApplication7.Application.Actors.Products.Search;
using ConsoleApplication7.Application.Search.Products;
using Newtonsoft.Json;
using Slalom.Stacks;
using Slalom.Stacks.Data.EntityFramework;
using Slalom.Stacks.Data.MongoDb;
using Slalom.Stacks.Logging.ApplicationInsights;
using Slalom.Stacks.Logging.EventHub;
using Slalom.Stacks.Logging.Serilog;
using Slalom.Stacks.Logging.SqlServer;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new ApplicationContainer(typeof(Program)))
            {
                container.UseSqlServerLogging();
                container.UseMongoDbRepositories();
                container.UseApplicationInsightsMetrics();
                container.UseSerilogDiagnostics();
                container.UseEntityFrameworkSearch();
                container.UseEventHubLogging(e =>
                {
                    e.WithConnection("Endpoint=sb://slalom-stacks.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=F6T4184DOxraeZi72ZBtnqUuNX2P4kLt9xpOmNw8UaA=;")
                     .WithEventHubNames("events", "requests");
                });

                container.Commands.SendAsync(new IndexProductsCommand()).Wait();

                var result = container.Commands.SendAsync(new AddProductCommand("aadfaddssfs", "")).Result;
                result = container.Commands.SendAsync(new SearchProductsCommand("8d8d00006a6a6400af2508d4417fe3f0")).Result;

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                Console.WriteLine("...");
                Console.ReadKey();
            }
        }
    }
}