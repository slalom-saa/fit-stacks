using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public class A : Command
        {
            public string Name { get; set; }
        }

        public class B : UseCaseActor<A, string>
        {
            public override string Execute(A command)
            {
                return "asdf";
            }
        }

        public static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(Program)))
            {
                var result = stack.SendAsync(new A()).Result;

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }
    }
}