using System;
using System.Linq;
using System.Security.Claims;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.ConsoleClient.Application.Products.Add;
using Slalom.Stacks.ConsoleClient.Aspects;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.TestStack.Examples.Actors.Items.Add;
using Slalom.Stacks.TestStack.Examples.Domain;

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
                    //builder.RegisterInstance(new EventStore()).As<IEventStore>();
                    //builder.RegisterInstance(new RequestStore()).As<IRequestStore>();
                });
                stack.UseSimpleConsoleLogging();

                if (stack.Send("products/add", new AddProductCommand("banme", -1)).Result.IsSuccessful)
                {
                    stack.Send("products/publish", "{}").Wait();
                }

                //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                Console.WriteLine("Complete");
                Console.ReadKey();
            }
        }
    }
}