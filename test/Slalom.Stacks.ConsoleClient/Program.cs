using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    [Subscribe("ProductAdded")]
    public class AddSomethingOnProductAdded : EndPoint<ProductAdded2>
    {
        public override void Receive(ProductAdded2 instance)
        {
            Console.WriteLine("ProductAdded2");
        }
    }

    [Subscribe("ProductAdded")]
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

    public class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack(typeof(AddProductCommand)))
                {
                    var content = @"[{
    ""requestId"": ""8d8d0000-6a6a-6400-307b-08d490c42a67"",
    ""id"": ""8d8d0000-6a6a-6400-84f8-08d490c42a6a"",
    ""timeStamp"": ""2017-05-01T18:59:16.5696431+00:00"",
    ""body"": {
      ""name"": ""name""
    },
    ""messageType"": ""ConsoleApp9.ProductAdded, ConsoleApp9, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""name"": ""ProductAdded""
  }]";

                    var messages = JsonConvert.DeserializeObject<JObject[]>(content);
                    foreach (var message in messages)
                    {
                        var name = message["name"].Value<string>();
                        stack.Publish(name, message.ToString());
                    }

                    Console.ReadKey();

                    //stack.Send(new AddProductCommand("name", "esc")).Wait();

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