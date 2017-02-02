using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication7.Application.Actors.Products.Add;
using ConsoleApplication7.Application.Actors.Products.Index;
using ConsoleApplication7.Application.Actors.Products.Search;
using Slalom.Stacks;
using Slalom.Stacks.Data.EntityFramework;
using Slalom.Stacks.Data.MongoDb;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging.ApplicationInsights;
using Slalom.Stacks.Logging.EventHub;
using Slalom.Stacks.Logging.Serilog;
using Slalom.Stacks.Logging.SqlServer;
using Slalom.Stacks.Runtime;
using ExecutionContext = Slalom.Stacks.Runtime.ExecutionContext;

namespace ConsoleApplication7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var container = new ApplicationContainer(typeof(Program)))
            {
                container.UseSqlServerLogging();

                Task.Run(() => new UserRunner(container).Run());

                Console.WriteLine("Running application.  Press any key to halt...");
                Console.ReadLine();
            }
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }

        public static async void Start()
        {
            try
            {
                var random = new Random(DateTime.Now.Millisecond);

                using (var container = new ApplicationContainer(typeof(Program)))
                {
                    container.UseSqlServerLogging();
                    //container.UseMongoDbRepositories();
                    //container.UseApplicationInsightsMetrics();
                    //container.UseSerilogDiagnostics();
                    //container.UseEntityFrameworkSearch();
                    //container.UseEventHubLogging(e =>
                    //{
                    //    e.WithConnection(
                    //            "Endpoint=sb://slalom-stacks.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=F6T4184DOxraeZi72ZBtnqUuNX2P4kLt9xpOmNw8UaA=;")
                    //        .WithEventHubNames("events", "requests");
                    //});

                    for (int i = 0; i < 1000; i++)
                    {
                        var next = random.Next(1000);
                        if (next % 3 == 0)
                        {
                            ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Administrator"), new Claim(ClaimTypes.Name, "administrator@example.com") }));
                        }
                        else if (next % 3 == 1)
                        {
                            ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "User"), new Claim(ClaimTypes.Name, "user@example.com") }));
                        }
                        else
                        {
                            ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Support"), new Claim(ClaimTypes.Name, "support@example.com") }));
                        }
                        await container.Commands.SendAsync(new AddProductCommand("aadfaddssfs", ""));
                        await container.Commands.SendAsync(new IndexProductsCommand());
                        await container.Commands.SendAsync(new SearchProductsCommand("8d8d00006a6a6400af2508d4417fe3f0"));
                    }
                }
                Console.WriteLine("Successfully completed.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }

    public class RunnerExecutionContextResolver : IExecutionContextResolver
    {
        private readonly string _userName;
        private readonly string _role;
        private readonly string _sourceAddress;

        public RunnerExecutionContextResolver(string userName, string role, string sourceAddress)
        {
            _userName = userName;
            _role = role;
            _sourceAddress = sourceAddress;
        }

        public ExecutionContext Resolve()
        {
            return new ExecutionContext("Runner", "Local", null, Guid.NewGuid().ToString("D"),
                 Guid.NewGuid().ToString("D"),
                 new ClaimsPrincipal(
                     new ClaimsIdentity(new[]
                     {
                        new Claim(ClaimTypes.Role, _role),
                        new Claim(ClaimTypes.Name, _userName)
                     })),
                 _sourceAddress, "", Environment.CurrentManagedThreadId);
        }
    }

    public class Runner
    {
        public static Random Random = new Random(DateTime.Now.Millisecond);
    }



    public class UserRunner
    {
        private readonly ApplicationContainer _container;

        public UserRunner(ApplicationContainer container, string userName = "user@stacks.io", string role = "User", string sourceAddress = "172.58.40.83")
        {
            _container = container.Copy();
            _container.Register<IExecutionContextResolver>(
                c => new RunnerExecutionContextResolver(userName, role, sourceAddress));
        }

        public void Run()
        {
            while (true)
            {
                ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "User"), new Claim(ClaimTypes.Name, "user@example.com") }));
                if (Runner.Random.Next(2) % 2 == 0)
                {
                    _container.Commands.SendAsync(new AddProductCommand("Product " + Runner.Random.Next(100), "")).Wait();
                }
                else
                {
                    _container.Commands.SendAsync(new SearchProductsCommand(Runner.Random.Next(100).ToString())).Wait();
                }
                Thread.Sleep(1 * Runner.Random.Next(5000));
            }
        }
    }
}