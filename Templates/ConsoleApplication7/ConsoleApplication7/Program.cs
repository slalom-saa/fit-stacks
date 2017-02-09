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
                Task.Run(() => new UserRunner().Run());
                Task.Run(() => new UserRunner("admin@stacks.com").Run());
                Task.Run(() => new UserRunner("support@stacks.com").Run());

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
        private ClaimsPrincipal _user;

        public RunnerExecutionContextResolver(string userName, string role)
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, role), new Claim(ClaimTypes.Name, userName) }));
        }

        public ExecutionContext Resolve()
        {
            return new ExecutionContext("Runner", "Local", null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), _user, "71.197.137.82", "local", Environment.CurrentManagedThreadId);
        }
    }

    public static class RunnerRandom
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        public static int Next(int max)
        {
            return _random.Next(max);
        }
    }

    public class UserRunner
    {
        private readonly ApplicationContainer _container;

        public UserRunner(string userName = "user@stacks.com", string role = "User")
        {
            _container = new ApplicationContainer(this);
            _container.Register<IExecutionContextResolver>(new RunnerExecutionContextResolver(userName, role));
            _container.UseSqlServerLogging();
        }

        public void Run()
        {
            while (true)
            {
                if (RunnerRandom.Next(2) % 2 == 0)
                {
                    _container.Commands.SendAsync(new AddProductCommand("Product " + RunnerRandom.Next(100), "")).Wait();
                }
                else
                {
                    _container.Commands.SendAsync(new SearchProductsCommand(RunnerRandom.Next(100).ToString())).Wait();
                }
                Thread.Sleep(1 * RunnerRandom.Next(5000));
            }
        }
    }
}