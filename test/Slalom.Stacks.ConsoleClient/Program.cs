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
using Slalom.Stacks.Services.Validation;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient
{
    public class Request
    {
        [NotNull("Content")]
        public string Name { get; }

        public Request(string name)
        {
            this.Name = name;
        }
    }

    [EndPoint("request")]
    public class RequestEndPoint : EndPoint<Request>
    {
        public override void Receive(Request instance)
        {
            //Console.WriteLine(instance.Name);
        }
    }


    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.Send("request").Result.OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}