using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Test.Examples;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;

namespace Slalom.Stacks.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ICommand command = new AddItemCommand("s");
            command.SetExecutionContext(new ExecutionContext("", "", "", "", "", null, "", "", 1));

            var audit = new LogEntry(command, new Messaging.CommandResult(command));

            Console.WriteLine(audit.Payload);

            return;


            Task.Run(() => new ExampleRunner().Start());
            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }
    }
}