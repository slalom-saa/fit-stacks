using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Communication.Logging;

namespace Slalom.FitStacks.ConsoleClient.Search
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

            optionsBuilder.UseSqlServer(_connectionString ?? "Data Source=localhost;Initial Catalog=Search;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemSearchResult>()
                        .ToTable("Items")
                        .HasKey(e => e.Id);

            //modelBuilder.Entity<Audit>()
            //            .ToTable("Audits")
            //            .HasKey(e => e.Id);

            //modelBuilder.Entity<Log>()
            //            .ToTable("Logs")
            //            .HasKey(e => e.Id);
        }
    }

}
