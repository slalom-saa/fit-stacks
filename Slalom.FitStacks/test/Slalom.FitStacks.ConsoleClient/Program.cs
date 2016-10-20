using System;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Domain;
using Slalom.FitStacks.Logging;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Mongo;
using Slalom.FitStacks.Runtime;
using System.Linq;

namespace Slalom.FitStacks.ConsoleClient
{
    public class Program
    {
        public class Item : Entity, IAggregateRoot
        {
            public string Name { get; set; }

            public Item(string name)
            {
                this.Name = name;
            }
        }

        public class ItemRepository : MongoRepository<Item>
        {
            public ItemRepository() : base(null, "Items")
            {
            }
        }

        public async void Start()
        {
            using (var container = new Container(typeof(Program)))
            {
                container.Register<ExecutionContext>(new LocalExecutionContext("test.user"));
                container.RegisterModule(new MongoModule());

                var target = container.Resolve<IDomainFacade>();

                await target.ClearAsync<Item>();


                Console.WriteLine("Added");
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                new Program().Start();
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}