using System;
using System.Linq;
using Slalom.LeanStack.Configuration;
using Slalom.LeanStack.Messaging;

namespace Authentication.ExecutionContext
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var container = new Container(typeof(Program)))
            {
                container.Register<IExecutionContext>(new ExampleExecutionContext("test.user"));

                var bus = container.Resolve<IMessageBus>();

                var result = bus.Send(new TestCommand()).Result;

                Console.WriteLine(result.IsSuccessful);
            }
        }
    }
}