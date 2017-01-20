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
    public class ConsoleLogger : ILogger
    {
        public void Dispose()
        {

        }

        public void Debug(Exception exception, string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Debug(string template, params object[] properties)
        {
            Console.WriteLine(template, properties);
        }

        public void Error(Exception exception, string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Error(string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Information(Exception exception, string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Information(string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Verbose(Exception exception, string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Verbose(string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Warning(Exception exception, string template, params object[] properties)
        {
            throw new NotImplementedException();
        }

        public void Warning(string template, params object[] properties)
        {
            throw new NotImplementedException();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ClaimsPrincipal.ClaimsPrincipalSelector = () => new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Administrator"), new Claim(ClaimTypes.Name, "user@example.com") }));

            using (var container = new ApplicationContainer(typeof(AddItemCommand)))
            {
                //var result = container.Commands.SendAsync(new AddItemCommand("asdf")).Result;

                //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                container.Append(c => new ConsoleLogger());
                container.Append(c => new ConsoleLogger());


                var logger = container.Resolve<ILogger>();

                logger.Debug("Adf");


            }


            //new ExampleRunner().Start();
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }
    }
}