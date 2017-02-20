using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication10.Application.Products.Add;
using Newtonsoft.Json;
using Slalom.Stacks;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Logging.SqlServer;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication10
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(Program)))
            {
                stack.UseSimpleConsoleLogging();
                stack.UseSqlServerLogging();
                stack.UseAkka("local");

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
            }
        }
    }
}
