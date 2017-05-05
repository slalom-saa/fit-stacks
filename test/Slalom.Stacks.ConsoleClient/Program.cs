﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Text;
#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient
{

    [Request("child")]
    public class ChildRequest
    {
    }

    [EndPoint("parent")]
    public class Parent : EndPoint
    {
        public override void Receive()
        {
            var result = this.Send<Response>(new ChildRequest()).Result;

            Console.WriteLine(result);
        }
       
    }

    [EndPoint("child")]
    public class Child : EndPoint
    {
        public override void Receive()
        {
            this.Respond(JsonConvert.SerializeObject(new Response()));
        }
    }

    public class Response
    {
        public string Property { get; set; } = "abc";
    }



    public class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack(typeof(AddProductCommand)))
                {
                    stack.Send("parent").Result.Response.OutputToJson();

                    // stack.Send(new AddProductCommand("name", "esc")).OutputToJson();

                    //Console.WriteLine(new String('-', 10));

                    //stack.GetEvents().OutputToJson();

                    //Console.WriteLine(new String('-', 10));

                    //stack.GetRequests().OutputToJson();

                    //Console.WriteLine(new String('-', 10));

                    //stack.GetResponses().OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}