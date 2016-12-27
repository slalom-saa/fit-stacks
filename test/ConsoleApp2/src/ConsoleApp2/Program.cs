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
using Slalom.Stacks.Search;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program.Start();
            Console.ReadLine();
        }

        public static async void Start()
        {
            try
            {
                using (var container = new ApplicationContainer(typeof(Program)))
                {
                    //   new AutoFacDependencyResolver(container.RootContainer, system);

                    container.RegisterModule(new ActorModule(typeof(Program)));

                    var watch = new Stopwatch();
                    var count = 1000 * 2;


                    var result = await container.SendAsync(new AddProcedureCommand("s"));

                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                    watch.Start();

                    var tasks = new List<Task>(count);
                    Parallel.For(0, count, new ParallelOptions { MaxDegreeOfParallelism = 4 }, e =>
                    {
                        tasks.Add(container.SendAsync(new AddProcedureCommand("s")));

                    });
                    await Task.WhenAll(tasks);

                    //var result = await container.SendAsync(new AddProcedureCommand("s"));

                    //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                    watch.Stop();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Execution for {count:N0} items completed successfully in {watch.Elapsed} - {Math.Ceiling(count / watch.Elapsed.TotalSeconds):N0} per second.  Press any key to exit...");
                    Console.ResetColor();

                    Console.ReadLine();
                }
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
