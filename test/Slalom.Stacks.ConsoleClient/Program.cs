using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Slalom.FitStacks.ConsoleClient.Commands.AddItem;
using Slalom.Stacks.Configuration;

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
                using (var container = new ApplicationContainer(typeof(Program)))
                {
                    watch.Start();
                    for (var i = 0; i < 10000; i++)
                    {
                        await Task.Run(() => container.Bus.SendAsync(new AddItemCommand("testing " + DateTime.Now.Ticks)).ConfigureAwait(false));
                    }
                    watch.Stop();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Execution completed successfully in {watch.Elapsed}.  Press any key to exit...");
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