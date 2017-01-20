using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Test.Examples;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var container = new ApplicationContainer(typeof(AddItemCommand)))
            {
                var result = container.Commands.SendAsync(new AddItemCommand(null)).Result;

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }


            //new ExampleRunner().Start();
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }
    }
}