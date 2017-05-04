using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Text;
#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient
{
    //[Subscribe("ProductAdded")]
    //public class AddCommandEventHandler : IHandle
    //{
    //    public void Receive(ExecutionContext context)
    //    {
    //        Console.WriteLine("...");
    //    }

    //    public bool ShouldHandle(IMessage instance)
    //    {
    //        return true;
    //    }
    //}

    [Subscribe("ProductAssdded")]
    public class AddSomethingOnProductAdded : EndPoint<ProductAdded2>
    {
        public override void Receive(ProductAdded2 instance)
        {
            Console.WriteLine("ProductAdded2");
        }
    }

    [Subscribe("ProductAddssed")]
    public class AddSomethingOnProductAdded2 : EndPoint<ProductAdded>
    {
        public override void Receive(ProductAdded instance)
        {
            Console.WriteLine("ProductAdded");
        }
    }

    public class ProductAdded2 : Event
    {
        public string Description { get; set; }

        public string Name { get; set; }
    }

    public class Pub : IEventPublisher
    {
        public Task Publish(params EventMessage[] events)
        {
            Console.WriteLine("publishing" + events.Select(e => e.Name));
            return Task.FromResult(0);
        }
    }

    public class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack(typeof(AddProductCommand)))
                {


                    stack.Send(new AddProductCommand("name", "esc")).OutputToJson();

                    //Console.WriteLine(new String('-', 10));

                    //stack.GetEvents().OutputToJson();

                    //Console.WriteLine(new String('-', 10));

                    //stack.GetRequests().OutputToJson();

                    //Console.WriteLine(new String('-', 10));

                    //stack.GetResponses().OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}