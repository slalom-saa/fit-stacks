﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;
using Slalom.Stacks.Test.Examples.Actors.Items.Search;
using Slalom.Stacks.Test.Examples.Domain;
using Slalom.Stacks.Test.Examples.Search;

// ReSharper disable AccessToDisposedClosure

#pragma warning disable 4014

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Factory.StartNew(() => new Program().Start());
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }

        public async Task Start()
        {
            try
            {
                var watch = new Stopwatch();
                var count = 100000;
                using (var container = new ApplicationContainer(typeof(Item), this))
                {
                    watch.Start();

                    var tasks = new List<Task<CommandResult>>(count);
                    Parallel.For(0, count, new ParallelOptions { MaxDegreeOfParallelism = 4 }, e =>
                    {
                        tasks.Add(container.SendAsync(new AddItemCommand("asdf")));
                    });
                    await Task.WhenAll(tasks);

                    watch.Stop();

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