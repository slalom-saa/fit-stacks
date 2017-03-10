using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Text;

namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(AddProduct)))
            {
                stack.UseInMemoryPersistence();


                var result = stack.Send(new AddProductCommand(null)).Result;

                stack.Send("_systems/messaging/requests").Result.OutputToJson();

                Console.WriteLine(result.ToJson());
            }
        }
    }
}
