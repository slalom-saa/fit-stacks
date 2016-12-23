using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Caching;
using Slalom.Stacks.Test.Domain;

// ReSharper disable AccessToDisposedClosure

#pragma warning disable 4014

namespace Slalom.FitStacks.ConsoleClient
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
                var count = 100;
                using (var container = new ApplicationContainer(typeof(Program)))
                {
                    container.UseLocalCache();

                    watch.Start();

                    var item = Item.Create("asdf");

                    container.Domain.AddAsync(item);

                    for (int i = 0; i < 10; i++)
                    {
                        var current = await container.Domain.FindAsync<Item>(item.Id);
                    }

                    await container.Domain.UpdateAsync(item);

                    for (int i = 0; i < 10; i++)
                    {
                        var current = await container.Domain.FindAsync<Item>(item.Id);
                    }

                    await container.Domain.RemoveAsync(item);

                    for (int i = 0; i < 10; i++)
                    {
                        var current = await container.Domain.FindAsync<Item>(item.Id);
                    }

                    watch.Stop();
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