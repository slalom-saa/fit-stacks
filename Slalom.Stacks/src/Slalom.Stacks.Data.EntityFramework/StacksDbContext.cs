using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Logging;

namespace Slalom.Stacks.EntityFramework
{
    public class StacksDbContext : DbContext
    {
        public ILogger Logger { get; set; }

        public async Task EnsureMigrations()
        {
            if ((await this.Database.GetPendingMigrationsAsync()).Any())
            {
                this.Logger.Verbose($"Applying migrations to {this.GetType()}.");

                await this.Database.MigrateAsync();
            }
        }
    }
}
