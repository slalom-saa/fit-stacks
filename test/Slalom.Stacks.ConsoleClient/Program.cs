﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Documentation;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public class HelloWorldRequest
        {
            public string Name { get; }

            public HelloWorldRequest(string name)
            {
                this.Name = name;
            }
        }

        public class HelloWorldResponse
        {
            public string Goto { get; set; }
        }

        [EndPoint("hello")]
        public class HelloWorld : EndPoint<HelloWorldRequest, string>
        {
            public override string Receive(HelloWorldRequest instance)
            {
                return "Asdf";
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.Send(new HelloWorldRequest("name")).Result.OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}