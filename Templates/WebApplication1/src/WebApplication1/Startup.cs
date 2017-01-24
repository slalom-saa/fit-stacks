using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Slalom.Stacks;
using Slalom.Stacks.Caching;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging.SqlServer;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using WebApplication1.Search.Products;

namespace WebApplication1
{
    public class Startup
    {
        private static ApplicationContainer container;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            container = new ApplicationContainer(this);
            container.Populate(services);

            //container.UseMongoDbRepositories();
            container.Search.RebuildIndexAsync<ProductSearchResult>();
            container.UseSqlServerLogging();


            //container.UseLocalAzureConfiguration(e =>
            //{
            //    e.RavenDb.WithUrl("http://localhost:9001");
            //    e.Search.WithAutoAddSearchResults();
            //});

            //container.UseLocalCache();

            //container.Register(c => new WebExecutionContextResolver(c.Resolve<IConfiguration>(), c.Resolve<IHttpContextAccessor>()));

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
