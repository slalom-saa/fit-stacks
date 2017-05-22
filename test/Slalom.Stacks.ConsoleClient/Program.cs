using System;
using System.IO;
using Autofac;
using Slalom.Stacks.Services.OpenApi;
using Slalom.Stacks.Text;

#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    var result = stack.Send("_system/open-api").Result;

                    result.OutputToJson();

                    File.WriteAllText("output.json", result.Response as string);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}