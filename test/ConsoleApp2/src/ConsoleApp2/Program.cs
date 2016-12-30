using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Newtonsoft.Json;
using Slalom.Stacks.Actors;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Search;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Test.Commands.AddItem;
using Slalom.Stacks.Test.Commands.SearchItems;
using Slalom.Stacks.Test.Domain;
using Slalom.Stacks.Test.Search;

namespace Slalom.Stacks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Start();
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadLine();
        }

        public async void Start()
        {
            try
            {
                var watch = new Stopwatch();
                var count = 100000;
                using (var container = new ApplicationContainer(typeof(Item), this))
                {
                    container.UseAkka();

                    watch.Start();

                    var tasks = new List<Task<CommandResult>>(count);
                    Parallel.For(0, count, new ParallelOptions { MaxDegreeOfParallelism = 4 }, e =>
                    {
                        tasks.Add(container.SendAsync(new AddItemCommand("asdf")));
                    });
                    await Task.WhenAll(tasks);

                    watch.Stop();

                    var searchResultCount = ((IQueryable<ItemSearchResult>)(await container.SendAsync(new SearchItemsCommand())).Response).Count();
                    var entityCount = (await container.Domain.FindAsync<Item>(e => true)).Count();
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
