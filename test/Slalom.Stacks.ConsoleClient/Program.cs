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
using Slalom.Stacks.Services;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.ConsoleClient
{
    public class Send : UseCase<AddProductEvent>
    {
        public override void Execute(AddProductEvent command)
        {
            Console.WriteLine("SSS");
        }
    }

    public class Send2 : UseCase<AddProductEvent>
    {
        public override void Execute(AddProductEvent command)
        {
            Console.WriteLine("SSS2");
        }
    }

    public class Program
    {


        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.UseSimpleConsoleLogging();

                    //var comments = typeof(AddProductCommand).GetProperty("Name").GetComments();
                    //Console.WriteLine(comments);

                    //Console.WriteLine(stack.GetServices().ToJson());


                    //var service = stack.CreatePublicRegistry("http://localhost");
                    //Console.WriteLine(JsonConvert.SerializeObject(service, Formatting.Indented));

                    stack.Send("catalog/products/add", new AddProductCommand("asdf", "description").ToJson()).Wait();
                    stack.Send("catalog/products/add", new AddProductCommand("asdf", "description")).Wait();
                    stack.Send(new AddProductCommand("asdf", "description")).Wait();

                    Console.WriteLine("Complete");
                    Console.ReadKey();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}