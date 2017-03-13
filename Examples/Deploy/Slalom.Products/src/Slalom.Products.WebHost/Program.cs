using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Application;
using Slalom.Stacks.Text;
using Slalom.Stacks.Web.AspNetCore;
using Autofac;
using Slalom.Stacks.Logging.SqlServer;
using Slalom.Stacks.Messaging.Routing;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Products.WebHost
{
    [EndPoint("catalog/products/add")]
    public class AddProductHost : EndPointHost<AddProduct>
    {
        public AddProductHost(AddProduct service)
            : base(service)
        {
        }

        public override int Retries => 0;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(AddProduct), typeof(AddProductHost)))
            {
                stack.UseSqlServerLogging();

                stack.UseAkkaMessaging();

                stack.RunWebHost();
            }
        }
    }
}
