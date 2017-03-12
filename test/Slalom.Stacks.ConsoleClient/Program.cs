using System;
using System.Threading.Tasks;
using Slalom.Stacks.Documentation;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new DocumentStack())
                {
                    stack.WriteToConsole();
                }
                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}