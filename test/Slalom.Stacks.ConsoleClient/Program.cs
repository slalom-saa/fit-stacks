using System;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.ConsoleClient
{
    public class AddItemRequest : Command
    {
    }

    [EndPoint("items/add")]
    public class AddItem : UseCase<AddItemRequest>
    {
        public override void Execute(AddItemRequest command)
        {
            this.AddRaisedEvent(new AddItemEvent());
        }
    }

    public class AddItemEvent : Event
    {
    }

    public class ForwardOnAdded : EventUseCase<AddItemEvent>
    {
        public override void Execute(AddItemEvent command)
        {
            Console.WriteLine("xa");
        }
    }


    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                    stack.UseInMemoryPersistence();

                    stack.Send("v1/items/add").Wait();

                    //stack.Send("_systems/messaging/responses").Result.OutputToJson();

                    Console.WriteLine("Complete");
                    Console.ReadKey();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}