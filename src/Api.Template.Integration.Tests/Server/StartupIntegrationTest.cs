using Api.Common.WebServer.Server;
using Api.Template.ApplicationService.InjectionModules;
using Api.Template.DbEFCore.Repositories;
using Api.Template.Domain.InjectionModules;
using Api.Template.Infrastructure.InjectionModules;
using Api.Template.Integration.Tests.InjectionModules;
using Autofac;
using Api.Template.WebApi.Server.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;


namespace Api.Template.Integration.Tests.Server
{
    public class StartupIntegrationTest
    {
        public StartupIntegrationTest(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {            
            // IoC Container Module Registration
            builder.RegisterModule(new IoCModuleApplicationService());
            builder.RegisterModule(new IoCModuleIntegrationTest());
            builder.RegisterModule(new IoCModuleInfrastructure());
            builder.RegisterModule(new IoCAutoMapperModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<EfCoreDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("ApiDbConnection")); });

            services.AddMvc(options => { options.Filters.Add(typeof(ValidateModelAttribute)); })
                .AddApplicationPart(Assembly.Load("Api.Template.WebApi"))
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor accessor)
        {
            app.UseApiResponseWrapperMiddleware();
            app.UseMvc();
            UpdateDatabaseUsingEfCore(app);
        }

        private void UpdateDatabaseUsingEfCore(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade database = serviceScope
                    .ServiceProvider
                    .GetRequiredService<EfCoreDbContext>()
                    .Database;

                database.EnsureDeleted();
                database.Migrate();
            }
        }
    }
}