using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.DocumentDb;
using Slalom.FitStacks.Domain;
using Slalom.FitStacks.EntityFramework;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Mongo;
using Slalom.FitStacks.Runtime;
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

    public class ItemSearchIndex : EntityFrameworkSearchIndex<ItemSearchResult>
    {
        public ItemSearchIndex(SearchContext context)
            : base(context)
        {
        }

        public IDomainFacade Domain { get; set; }

        public override async Task RebuildIndexAsync()
        {
            await this.ClearAsync();

            var index = 0;
            var size = 1000;

            var set = this.Domain.CreateQuery<Item>();

            var working = set.Take(size).ToList();
            while (working.Any())
            {
                await this.AddAsync(working.Select(e => new ItemSearchResult()).ToArray());
                working = set.Skip(++index * size).Take(size).ToList();
            }
        }
    }

    public class Item : Entity, IAggregateRoot
    {
        public Item(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }

    public class ItemRepository : DocumentDbRepository<Item>
    {
        public ItemRepository()
            : base(null, "Items")
        {
        }

        public override Task AddAsync(Item[] instances)
        {
            return base.AddAsync(instances);
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
        public static void Main(string[] args)
        {
            new Program().Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public async void Start()
        {
            try
            {
                using (var container = new Container(typeof(Program)))
                {
                    container.Register<ExecutionContext>(new LocalExecutionContext("test.user"));
                    container.RegisterModule(new DocumentDbDomainModule());
                    container.RegisterModule(new EntityFrameworkSearchModule());
                    container.Register<ISearchIndex<ItemSearchResult>>(c => new ItemSearchIndex(c.Resolve<SearchContext>()));
                    container.Register(new SearchContext("Data Source=localhost;Initial Catalog=Fit;Integrated Security=True"));

                    container.Configure<DocumentDbOptions>("DocumentDb");

                    //await container.Search.RebuildIndexAsync<ItemSearchResult>();
                    await container.Bus.Send(new CreateItemCommand());

                    Console.WriteLine(container.Domain.CreateQuery<Item>().Select(e => 1).ToList().Count());

                    var query = container.Search.CreateQuery<ItemSearchResult>();

                    Console.WriteLine(query.Count());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }

    public class ItemCreatedEvent : Event
    {
    }
}