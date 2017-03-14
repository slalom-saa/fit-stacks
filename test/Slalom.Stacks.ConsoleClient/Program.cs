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
                using (var stack = new Stack())
                {
                    stack.Send("_systems/services").Result.OutputToJson();


                    //var path = @"C:\source\Stacks\Core\src\Slalom.Stacks.Documentation\output.docx";

                    //stack.CreateWordDocument(path);
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