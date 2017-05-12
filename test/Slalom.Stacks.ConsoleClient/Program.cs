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
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}