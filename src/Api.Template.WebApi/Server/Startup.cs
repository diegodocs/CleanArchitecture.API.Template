using Api.Common.WebServer.Server;
using Api.Template.ApplicationService.InjectionModules;
using Api.Template.DbEFCore.Repositories;
using Api.Template.Domain.InjectionModules;
using Api.Template.Infrastructure.InjectionModules;
using Autofac;
using Api.Template.WebApi.Server.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using NLog.Web;
using Api.Common.Mock.Infrastructure.InjectionModules;

namespace Api.Template.WebApi.Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env,
            ILogger<Startup> logger)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            env.ConfigureNLog("nlog.config");

            logger.LogInformation($"Starting ... {env.ApplicationName}");            
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // IoC Container Module Registration
            builder.RegisterModule(new IoCModuleApplicationService());
            builder.RegisterModule(new IoCModuleMockInfra());
            builder.RegisterModule(new IoCAutoMapperModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<EfCoreDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("ApiDbConnection")); });

            services
                .AddMvc(options => { options.Filters.Add(typeof(ValidateModelAttribute)); })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info { Title = "Api.Template Swagger Documentation", Version = "v1" });                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();            
            app.UseApiResponseWrapperMiddleware();

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            // Enable middleware to serve swagger-ui assets(HTML, JS, CSS etc.)
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.Template Documentation");
            });

            //Execute Database Migration
            //UpdateDatabaseUsingEfCore(app);
        }

        //private void UpdateDatabaseUsingEfCore(IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        //    {
        //        serviceScope
        //            .ServiceProvider
        //            .GetRequiredService<EfCoreDbContext>()
        //            .Database
        //            .Migrate();
        //    }
        //}
    }
}