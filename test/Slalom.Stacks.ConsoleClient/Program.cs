using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Test.Examples;
using Slalom.Stacks.Test.Examples.Domain;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Task.Run(() => new ExampleRunner().Start());
            //Console.WriteLine("Running application.  Press any key to halt...");
            //Console.ReadKey();

            using (var container = new ApplicationContainer(typeof(Item)))
            {
                var result = container.Commands.SendAsync("items/add", "{text:'something'}").Result;

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }
    }
}