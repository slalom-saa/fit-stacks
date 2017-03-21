using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Documentation;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public class HelloWorldRequest
        {
            public string Name { get; }

            public HelloWorldRequest(string name)
            {
                this.Name = name;
            }
        }

        public class HelloWorldResponse
        {
            public string Goto { get; set; }
        }

        [EndPoint("hello")]
        public class HelloWorld : EndPoint<HelloWorldRequest>
        {
            public override void Receive(HelloWorldRequest instance)
            {
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    EndPointMetaData.Create(typeof(HelloWorld)).OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}