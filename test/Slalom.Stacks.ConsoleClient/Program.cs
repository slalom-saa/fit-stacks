using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Configuration.Actors;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.TestKit;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient
{

    public class Go
    {
    }

    public class There
    {
    }


    public class S : Service, IEndPoint<Go, There>, IEndPoint<There>
    {
        [EndPoint("go")]
        public Task<There> Receive(Go instance)
        {
            return Task.FromResult(new There());
        }

        [EndPoint("there")]
        public Task Receive(There instance)
        {
            return Task.FromResult(0);
        }
    }



    //public class b : BusinessRule<AddProductCommand>
    //{
    //    public override IEnumerable<ValidationError> Validate(AddProductCommand instance)
    //    {
    //        yield return "XXX";
    //    }
    //}

    //public class s_rule : SecurityRule<AddProductCommand>
    //{
    //    public override IEnumerable<ValidationError> Validate(AddProductCommand instance)
    //    {
    //        //if (!this.User.Identity.IsAuthenticated)
    //        //{
    //        //    yield return "allo";
    //        //}
    //        yield break;
    //    }
    //}

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.UseInMemoryPersistence();

                    stack.Send("go").Result.OutputToJson();

                    stack.Send(new AddProductCommand("name", "")).Result.OutputToJson();

                    Clipboard.SetText(stack.Send("_systems/messaging/requests").Result.ToJson());

                    Console.WriteLine("Complete");
                    Console.ReadKey();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}