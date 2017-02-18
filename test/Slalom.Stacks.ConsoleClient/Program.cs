using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Messaging.Serialization;
using Slalom.Stacks.TestStack.Examples.Actors.Items.Add;
using Slalom.Stacks.TestStack.Examples.Domain;

namespace Slalom.Stacks.ConsoleClient
{
    public class Product : AggregateRoot
    {
        public string Name { get; set; }

        public Product(string name)
        {
            this.Name = name;
            this.AddEvent(new ProductAddedEvent(this.Name));
        }
    }

    public class ProductAddedEvent : Event
    {
        public string Name { get; }

        public ProductAddedEvent(string name)
        {
            this.Name = name;
        }
    }

    public class AddProductCommand : Command
    {
        public string Name { get; }

        public AddProductCommand(string name)
        {
            this.Name = name;
        }
    }

    public class AddProductEvent : Event
    {
    }

    public class AddProduct : UseCaseActor<AddProductCommand, AddProductEvent>
    {
        public override async Task<AddProductEvent> ExecuteAsync(AddProductCommand command)
        {
            await this.Domain.AddAsync(new Product("name"));

            return new AddProductEvent();
        }
    }

    public class EventStore : IAuditStore
    {
        public Task AppendAsync(AuditEntry audit)
        {
            Console.WriteLine(JsonConvert.SerializeObject(audit, Formatting.Indented));
            return Task.FromResult(0);
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack())
            {
                stack.AddMessagingTypes(typeof(Program));
                stack.Use(builder =>
                {
                    builder.RegisterInstance(new EventStore()).As<IAuditStore>();
                });

                var result = stack.SendAsync(new AddProductCommand("name")).Result;

               // Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                Console.ReadKey();
            }
        }
    }
}