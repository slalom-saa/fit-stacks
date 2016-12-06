using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Communication.Logging;

namespace Slalom.Stacks.EntityFramework
{
    public class LoggingContext : StacksDbContext
    {
        private readonly string _connectionString;

        public LoggingContext(string connectionString)
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

            modelBuilder.Entity<Audit>()
                        .ToTable("Audits")
                        .HasKey(e => e.Id);

            modelBuilder.Entity<Log>()
                        .ToTable("Logs")
                        .HasKey(e => e.Id);
        }
    }
}