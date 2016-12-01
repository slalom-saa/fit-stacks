using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Slalom.Stacks.Communication.Logging;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Data.EntityFramework;
using Slalom.Stacks.Runtime;
using Module = Autofac.Module;

namespace Slalom.Stacks.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = this.Configuration["Authority"],
                ScopeName = "api",
                RequireHttpsMetadata = false
            });

            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            var builder = new ContainerBuilder();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.RegisterModule(new StacksModule(this));

            builder.RegisterModule(new StacksWebApiModule());

            builder.Register(c => new SearchContext());
            builder.RegisterModule(new LoggingModule<SearchContext>());

            builder.Populate(services);

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }
    }

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

            modelBuilder.Entity<Audit>()
                        .ToTable("Audits")
                        .HasKey(e => e.Id);

            modelBuilder.Entity<Log>()
                        .ToTable("Logs")
                        .HasKey(e => e.Id);
        }
    }

    public class StacksWebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new WebExecutionContextResolver(c.Resolve<IConfiguration>(), c.Resolve<IHttpContextAccessor>())).As<IExecutionContextResolver>();
        }
    }
}