using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.ConsoleClient.Application.Products.Add;
using Slalom.Stacks.ConsoleClient.Aspects;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Serialization;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(Program)))
            {
                stack.Use(builder =>
                {
                    builder.RegisterInstance(new EventStore()).As<IEventStore>();
                    //builder.RegisterInstance(new RequestStore()).As<IRequestStore>();
                });
                stack.UseSimpleConsoleLogging();

                stack.Send("products/add", new AddProductCommand("banme", 15)).Wait();
                //stack.Send("products/publish", "{}").Wait();

                //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                Console.WriteLine("Complete");
                Console.ReadKey();
            }
        }
    }
}