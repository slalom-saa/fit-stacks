using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.TestStack.Examples.Actors.Items.Add;
using Slalom.Stacks.TestStack.Examples.Actors.Items.Search;
using Slalom.Stacks.TestStack.Examples.Domain;
using Slalom.Stacks.TestStack.Examples.Search;
using System.Linq;
using Newtonsoft.Json;

namespace Slalom.Stacks.TestStack.Examples
{
        

    public class ExampleRunner
    {
        private readonly object[] _indicators;
        private Action<Stack> _configuration;

        public ExampleRunner(params object[] indicators)
        {
            _indicators = indicators.Union(new object[] { typeof(ExampleRunner) }).ToArray();
        }

        public ExampleRunner With(Action<Stack> configuration)
        {
            _configuration = configuration;
            return this;
        }

        public void Start(int count = 2000)
        {
            Task.Run(async () =>
            {
                try
                {
                    var watch = new Stopwatch();
                    using (var container = new Stack(_indicators))
                    {
                        _configuration?.Invoke(container);

                        ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Administrator"), new Claim(ClaimTypes.Name, "user@example.com") }));

                        watch.Start();

                        var tasks = new List<Task<CommandResult>>(count);
                        Parallel.For(0, count, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, e =>
                        {
                            tasks.Add(container.SendAsync("items/add", new AddItemCommand(e.ToString())));
                        });
                        await Task.WhenAll(tasks);

                        watch.Stop();

                        var failed = tasks.Where(e => !e.Result.IsSuccessful).Select(e => e.Result).ToList();
                        if (failed.Any())
                        {
                            throw new Exception($"{failed.Count()} of the results were not successful. The first was \n"
                                + JsonConvert.SerializeObject(failed.First(), Formatting.Indented));
                        }

                        var searchResultCount = ((IQueryable<ItemSearchResult>)(await container.SendAsync(new SearchItemsCommand())).Response).Count();
                        var entityCount = (await container.Domain.FindAsync<Item>()).Count();
                        if (entityCount != count || searchResultCount != count)
                        {
                            throw new Exception($"The execution did not have the expected results. {searchResultCount} search results and {entityCount} entities out of {count}.");
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Execution for {count:N0} items completed successfully in {watch.Elapsed} - {Math.Ceiling(count / watch.Elapsed.TotalSeconds):N0} per second.  Press any key to exit...");
                    Console.ResetColor();
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exception);
                    Console.ResetColor();
                }
            });
        }
    }
}
