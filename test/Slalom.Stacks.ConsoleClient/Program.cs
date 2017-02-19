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
using Slalom.Stacks.Validation;

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
        [NotNull("no")]
        public string Name { get; }

        public AddProductCommand(string name)
        {
            this.Name = name;
        }
    }
    

    public class AddProductEvent : Event
    {
    }

    [Path("products/add")]
    public class AddProduct : Actor<AddProductCommand, Product>
    {
        public override async Task<Product> ExecuteAsync(AddProductCommand message)
        {
            var target = new Product("name");

            await this.Domain.AddAsync(target);

            return target;
        }
    }

    public class EventStore : IEventStore
    {
        public Task AppendAsync(EventEntry entry)
        {
            Console.WriteLine(JsonConvert.SerializeObject(entry, Formatting.Indented));

            return Task.FromResult(0);
        }
    }

    public class RequestStore : IRequestStore
    {
        public Task AppendAsync(RequestEntry entry)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(entry, Formatting.Indented));

            return Task.FromResult(0);
        }
    }

    public class SendEmailOnProductAdded : Actor<ProductAddedEvent>
    {
        public override void Execute(ProductAddedEvent message)
        {
            //Console.WriteLine("Sending mail.");
        }
    }

    public class SendOtherOnProductAdded : Actor<ProductAddedEvent>
    {
        public override void Execute(ProductAddedEvent message)
        {
            //Console.WriteLine("Sending other.");
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(Program)))
            {
                stack.Use(builder =>
                {
                    builder.RegisterInstance(new EventStore()).As<IEventStore>();
                    builder.RegisterInstance(new RequestStore()).As<IRequestStore>();
                });

                var result = stack.SendAsync("products/add", new AddProductCommand("banme")).Result;

                //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                Console.WriteLine("Complete");
                Console.ReadKey();
            }
        }
    }
}