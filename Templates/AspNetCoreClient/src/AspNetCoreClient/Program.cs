using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreClient.Application.Products.Add;
using Slalom.Stacks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Registration;
using Slalom.Stacks.Web.AspNetCore;

namespace AspNetCoreClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack())
            {
                stack.RunHost();
            }
        }
    }
}
