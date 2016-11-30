using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Communication.Validation;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;
using Slalom.Stacks.EntityFramework;
using Slalom.Stacks.Logging.Serilog;
using Slalom.Stacks.Mongo;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient
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

            modelBuilder.Entity<Audit>()
                        .ToTable("Audits")
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

    public class ItemRepository : MongoRepository<Item>
    {
        public ItemRepository(ItemMongoContext context) : base(context)
        {
        }
    }

    public class ItemSearchResult : ISearchResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public class CreateItemCommand : Communication.Command<ItemCreatedEvent>
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

    public class ItemMongoContext : MongoDbContext
    {
        public ItemMongoContext()
        {
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

    public class must_do_this : BusinessValidationRule<CreateItemCommand>
    {
        protected override async Task<ValidationError> Validate(CreateItemCommand instance)
        {
            return "asdf";
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
                    container.RegisterModule(new SerilogModule());
                    container.Register<IAuditStore>(c => new EntityFrameworkAuditStore(new SearchContext()));

                    new SearchContext().Database.Migrate();

                    var result = container.Bus.Send(new CreateItemCommand()).Result;

                    Console.WriteLine(result.IsSuccessful);
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