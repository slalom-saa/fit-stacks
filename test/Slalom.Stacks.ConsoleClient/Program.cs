using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Test.Examples;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Administrator"), new Claim(ClaimTypes.Name, "user@example.com") }));

            using (var container = new ApplicationContainer(typeof(AddItemCommand)))
            {
                var result = container.Commands.SendAsync("items/add", "{}").Result;

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            
            //new ExampleRunner().Start();
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }
    }
}