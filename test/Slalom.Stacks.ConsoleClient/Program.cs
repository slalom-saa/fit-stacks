using System;
using System.Threading.Tasks;
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
        public class Product : AggregateRoot
        {
        }

        public class AddProductCommand : Command
        {
        }

        public class AddProduct : UseCase<AddProductCommand, string>
        {
            public override async Task<string> ExecuteAsync(AddProductCommand command)
            {
                var target = new Product();

                await this.Domain.Add(target);

                return target.Id;
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.Send(new AddProductCommand()).Result.OutputToJson();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}