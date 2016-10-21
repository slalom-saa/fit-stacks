using System;
using System.Collections.Generic;
using System.IO;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Domain;
using Slalom.FitStacks.Logging;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Mongo;
using Slalom.FitStacks.Runtime;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slalom.FitStacks.EntityFramework;
using Slalom.FitStacks.Search;

namespace Slalom.FitStacks.ConsoleClient
{

    public class SearchContext : DbContext
    {
        private readonly string _connectionString;

        public SearchContext()
        {
        }

        public SearchContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_connectionString ?? "Data Source=localhost;Initial Catalog=Fit;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemSearchResult>()
                        .ToTable("Items")
                        .HasKey(e => e.Id);
        }
    }

    public class ItemSearchResultStore : EntityFrameworkSearchResultStore<ItemSearchResult>
    {
        public IDomainFacade Domain { get; set; }

        public ItemSearchResultStore(SearchContext context)
            : base(context)
        {
        }

        public override async Task RebuildIndexAsync()
        {
            await this.ClearAsync();

            int index = 0;
            int size = 1000;

            var set = Domain.CreateQuery<Item>();

            var working = set.Take(size).ToList();
            while (working.Any())
            {
                await this.AddAsync(working.Select(e => new ItemSearchResult
                {
                }).ToArray());
                Console.WriteLine(index);
                working = set.Skip(++index * size).Take(size).ToList();
            }
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
        public override async Task<ItemCreatedEvent> Handle(CreateItemCommand command)
        {
            await this.Domain.AddAsync(new Item(command.CommandName));

            return new ItemCreatedEvent();
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
                    container.RegisterModule(new MongoDomainModule());
                    container.RegisterModule(new EntityFrameworkSearchModule());
                    container.Register<ISearchResultStore<ItemSearchResult>>(c => new ItemSearchResultStore(c.Resolve<SearchContext>()));
                    container.Register(new SearchContext("Data Source=localhost;Initial Catalog=Fit;Integrated Security=True"));

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
                    //await container.Bus.Send(new CreateItemCommand());

                    Console.WriteLine(container.Domain.CreateQuery<Item>().Count());


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