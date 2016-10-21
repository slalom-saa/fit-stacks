using System;
using System.Collections.Generic;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Domain;
using Slalom.FitStacks.Logging;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Mongo;
using Slalom.FitStacks.Runtime;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Slalom.FitStacks.Search;

namespace Slalom.FitStacks.ConsoleClient
{
    public class ItemSearchResultStore : ISearchStore<ItemSearchResult>
    {
        private readonly IDomainFacade _domain;
        private static List<ItemSearchResult> _instances = new List<ItemSearchResult>();

        public ItemSearchResultStore(IDomainFacade domain)
        {
            _domain = domain;
        }

        public async Task RebuildIndexAsync()
        {
            await this.ClearAsync();

            var items = _domain.CreateQuery<Item>();

            int index = 0;
            var set = items.Skip(1000 * index).Take(1000);
            
            while (set.Any())
            {
                await this.AddAsync(set.Select(e => new ItemSearchResult
                {
                    Id = e.Id
                }).ToArray());
                set = items.Skip(1000 * ++index).Take(1000);
            }
        }

        public Task AddAsync(ItemSearchResult[] instances)
        {
            _instances.AddRange(instances);

            return Task.FromResult(0);
        }

        public Task ClearAsync()
        {
            _instances.Clear();

            return Task.FromResult(0);
        }

        public Task DeleteAsync(ItemSearchResult[] instances)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<ItemSearchResult, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ItemSearchResult> CreateQuery()
        {
            return _instances.AsQueryable();
        }

        public Task<ItemSearchResult> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ItemSearchResult[] instances)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Expression<Func<ItemSearchResult, bool>> predicate, Expression<Func<ItemSearchResult, ItemSearchResult>> expression)
        {
            throw new NotImplementedException();
        }
    }

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

    public class ItemSearchResult : ISearchResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public class CreateItemCommand : Command<ItemCreatedEvent>
    {
    }

    public class CreateItemCommandHandler : CommandHandler<CreateItemCommand, ItemCreatedEvent>
    {
        public override Task<ItemCreatedEvent> Handle(CreateItemCommand command)
        {
            return Task.FromResult(new ItemCreatedEvent());
        }
    }


    public class ItemSearchResultUpdater : IHandleEvent<ItemCreatedEvent>
    {
        private readonly ISearchFacade _facade;

        public ItemSearchResultUpdater(ISearchFacade facade)
        {
            _facade = facade;
        }

        public Task Handle(ItemCreatedEvent instance, ExecutionContext context)
        {
            return _facade.AddAsync(new ItemSearchResult());
        }
    }

    public class Program
    {


        public async void Start()
        {
            try
            {
                using (var container = new Container(typeof(Program)))
                {
                    container.Register<ExecutionContext>(new LocalExecutionContext("test.user"));
                    container.RegisterModule(new MongoModule());
                    //container.Register<ISearchFacade>(new SearchFacade());

                    //await container.Domain.AddAsync(new Item("asdf"));

                    //await container.Bus.Send(new CreateItemCommand());


                    //await container.Domain.AddAsync(new Item("ddd"));

                    //Console.WriteLine(container.Domain.CreateQuery<Item>().Count());


                    //var target = new List<Item>();
                    //for (int i = 0; i < 10000; i++)
                    //{
                    //    target.Add(new Item("Item " + i));
                    //}
                    //await container.Domain.AddAsync(target);



                    await container.Search.RebuildIndexAsync<ItemSearchResult>();
                    await container.Bus.Send(new CreateItemCommand());

                    //Console.WriteLine(container.Domain.CreateQuery<Item>().Count());


                     var query = container.Search.CreateQuery<ItemSearchResult>();

                     Console.WriteLine(query.Count());

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public static void Main(string[] args)
        {
            new Program().Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    public class ItemCreatedEvent : Event
    {
    }
}