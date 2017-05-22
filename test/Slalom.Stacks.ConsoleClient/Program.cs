using System;
using Autofac;
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
                "products".ToTitle().OutputToJson();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}