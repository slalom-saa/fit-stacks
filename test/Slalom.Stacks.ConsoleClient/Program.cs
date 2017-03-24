using System;
using Slalom.Stacks.Messaging;
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

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.Send(new HelloWorldRequest("name")).Wait();

                    stack.GetResopnses().OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}