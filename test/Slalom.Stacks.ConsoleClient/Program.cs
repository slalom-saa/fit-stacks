﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.ConsoleClient.Application.Products.Add;
using Slalom.Stacks.ConsoleClient.Aspects;
using Slalom.Stacks.Documentation;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.Services;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new DocumentStack(typeof(AddProduct)))
                {
                    stack.WriteToConsole();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}