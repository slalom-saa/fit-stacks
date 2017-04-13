using System;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Registry;
using Slalom.Stacks.TestKit;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public class HelloWorldRequest
        {
            public HelloWorldRequest(string name)
            {
                this.Name = name;
            }

            public string Name { get; }
        }

        public class HelloWorldResponse
        {
            public string Goto { get; set; }
        }

        [EndPoint("hello")]
        public class HelloWorld : EndPoint<HelloWorldRequest, string>
        {
            public override string Receive(HelloWorldRequest instance)
            {
                return "Asdf";
            }
        }

        [TestSubject(typeof(HelloWorld))]
        public class When_submitting_hello_world
        {
            public void should_do_this()
            {
            }

            public void should_do_that()
            {
            }
        }

       

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new TestStack(typeof(Program)))
                {

                    stack.UseEndPoint<SendNotification>(e =>
                    {
                        Console.WriteLine("A");
                    });

                    //stack.UseEndPoint("catalog/products/add", e =>
                    //{
                    //    Console.WriteLine(";");
                    //});

                    stack.Send(new AddProductCommand("name", "e"));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}