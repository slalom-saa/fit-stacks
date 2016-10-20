using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Runtime;
using Module = Autofac.Module;

namespace Slalom.FitStacks.WebApi
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

            builder.RegisterModule(new FitStacksModule(this));

            builder.RegisterModule(new FitStacksWebApiModule());

            builder.Populate(services);

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }
    }

    public class FitStacksWebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new WebExecutionContextResolver(c.Resolve<IConfiguration>(), c.Resolve<IHttpContextAccessor>())).As<IExecutionContextResolver>();
        }
    }
}