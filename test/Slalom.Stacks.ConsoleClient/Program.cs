using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Domain;
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
            Start();
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }

        public static async void Start()
        {
            ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Administrator"), new Claim(ClaimTypes.Name, "user@example.com") }));

            using (var container = new Stack(typeof(AddItemCommand)))
            {
                var result = await container.SendAsync("items/add", "{}");

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                result = await container.SendAsync("items/add", "{name:\"No\"}");

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                result = await container.SendAsync("items/add", "{name:\"Now\"}");

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }
    }
}