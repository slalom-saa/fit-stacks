using System;
using System.Linq;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Communication.Logging;

namespace Slalom.Stacks.EntityFramework
{
    public class LoggingModule : Module
    {
        private readonly string _connectionString;

        public LoggingModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new LoggingDbContext(_connectionString));

            builder.Register<IAuditStore>(c => new AuditStore(c.Resolve<LoggingDbContext>()));
            builder.Register<ILogStore>(c => new LogStore(c.Resolve<LoggingDbContext>()));


            using (var context = new LoggingDbContext(_connectionString))
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
