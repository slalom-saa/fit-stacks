using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Security;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient
{
    public class Req
    {
        [Url("sdf")]
        public string Url { get; set; } = "http://localhost:5001";

        [Json("ad")]
        public string Json { get; set; } = "{}";
    }

    public class Go : EndPoint<Req>
    {
        public override void Receive(Req instance)
        {
            base.Receive(instance);
        }
    }


    public class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                var content = "test";

                var bytes = Encoding.UTF8.GetBytes(content);

                Encoding.UTF8.GetString(Encryption.Decrypt(Encryption.Encrypt(bytes))).OutputToJson(); ;


                return;

                using (var stack = new Stack(typeof(AddProductCommand)))
                {
                    var config = stack.Container.Resolve<IConfiguration>();

                    config.GetValue<string>("Authority").OutputToJson();

                    //stack.Send(new Req()).Result.OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}