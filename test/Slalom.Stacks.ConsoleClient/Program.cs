using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Test.Examples;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(() => new ExampleRunner().Start());
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }
    }
}