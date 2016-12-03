using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Communication.Logging;
using Slalom.Stacks.EntityFramework;

namespace Slalom.FitStacks.ConsoleClient.Search
{
    public class SearchContext : StacksDbContext
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

            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemSearchResult>()
                        .ToTable("Items")
                        .HasKey(e => e.Id);
        }
    }

}
