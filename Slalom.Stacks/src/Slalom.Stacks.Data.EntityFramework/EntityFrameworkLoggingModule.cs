using System;
using System.Linq;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Communication.Logging;
using Slalom.Stacks.Configuration;

namespace Slalom.Stacks.EntityFramework
{
    public class EntityFrameworkLoggingModule : Module
    {
        private readonly string _connectionString;

        public EntityFrameworkLoggingModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c =>
            {
                var context = new LoggingContext(_connectionString);
                c.InjectProperties(context, new AllUnsetPropertySelector());
                context.EnsureMigrations().Wait();
                return context;
            }).As<LoggingContext>();

            builder.Register<IAuditStore>(c => new AuditStore(c.Resolve<LoggingContext>()));
            builder.Register<ILogStore>(c => new LogStore(c.Resolve<LoggingContext>()));
        }
    }
}
