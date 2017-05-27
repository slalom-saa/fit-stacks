using System;
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
                    stack.Information.OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}