using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.ConsoleClient.Application.Products.Add;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Services;
using Slalom.Stacks.TestKit;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.ConsoleClient
{

    public class A : UseCase<ProductAdded>
    {
        public override void Execute(ProductAdded command)
        {
            Console.WriteLine("ADSFADF");
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
                    stack.UseInMemoryPersistence();
                    

                    stack.Send(new AddProductCommand("name", "des")).Wait();
                   // stack.Send(new AddProductCommand("nameasd", "desss")).Wait();
                  //  stack.Send(new AddProductCommand("namfe", "adsfdes")).Wait();

                    Console.WriteLine(stack.Send("_systems/requests").Result.Response.ToJson());




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