using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks;
using Slalom.Stacks.Documentation;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Routing;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Text;
using Slalom.Stacks.Web.AspNetCore;

namespace ConsoleClient
{
    [EndPoint("catalog/products/add")]
    public class AddProductHost : EndPointHost<AddProduct>
    {
        public AddProductHost(AddProduct service)
            : base(service)
        {
        }

        public override int Retries => 3;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(AddProduct), typeof(AddProductHost)))
            {
                //stack.UseSqlServerLogging();

                stack.UseAkkaMessaging();

                stack.RunWebHost();
            }
        }
    }
}
