using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;
using Slalom.Stacks.Test.Examples.Actors.Items.Search;
using Slalom.Stacks.Test.Examples.Domain;
using Slalom.Stacks.Test.Examples.Search;

namespace Slalom.Stacks.Test.Examples
{
    public class ExampleRunner
    {
        public async Task Start()
        {
            try
            {
                var watch = new Stopwatch();
                var count = 2000;
                using (var container = new ApplicationContainer(this))
                {
                    ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Administrator") }));

                    watch.Start();

                    var tasks = new List<Task<CommandResult>>(count);
                    Parallel.For(0, count, new ParallelOptions { MaxDegreeOfParallelism = 4 }, e =>
                    {
                        tasks.Add(container.SendAsync(new AddItemCommand(e.ToString())));
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
        }
    }
}
