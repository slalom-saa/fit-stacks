using System;
using System.Linq;
using Slalom.Stacks.Configuration;

namespace Slalom.FitStacks.ConsoleClient
{
    public class Program

    {
        public static void Main(string[] args)
        {
            new Program().Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public async void Start()
        {
            try
            {
                using (var container = new ApplicationContainer(typeof(Program)))
                {
                }

                Console.WriteLine("done");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}