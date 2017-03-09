using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Configuration.Actors;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.TestKit;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient
{
    [Command("catalog/products/add")]
    public class AddCommand : Command
    {
    }

    public class A : IHandle<ProductAdded>
    {
        public Task Handle(ProductAdded command)
        {
            Console.WriteLine("..ss.");

            return Task.FromResult(0);
        }
    }

    //public class b : BusinessRule<AddProductCommand>
    //{
    //    public override IEnumerable<ValidationError> Validate(AddProductCommand instance)
    //    {
    //        yield return "XXX";
    //    }
    //}

    //public class s_rule : SecurityRule<AddProductCommand>
    //{
    //    public override IEnumerable<ValidationError> Validate(AddProductCommand instance)
    //    {
    //        //if (!this.User.Identity.IsAuthenticated)
    //        //{
    //        //    yield return "allo";
    //        //}
    //        yield break;
    //    }
    //}

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.UseInMemoryPersistence();

                    //stack.UseSimpleConsoleLogging();


                    Console.WriteLine(stack.Send(new AddCommand()).Result.ToJson());

                    //Clipboard.SetText(stack.GetServices().CreatePublicRegistry("http://localhost").ToJson());

                    //stack.Send(new AddProductCommand("adf", "Adf")).Wait();

                    //Console.WriteLine(stack.Send("_systems/messaging/requests").Result.Response.ToJson());
                    //Console.WriteLine(stack.Send("_systems/messaging/responses").Result.Response.ToJson());
                    //Console.WriteLine(stack.Send("_systems/events").Result.Response.ToJson());


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