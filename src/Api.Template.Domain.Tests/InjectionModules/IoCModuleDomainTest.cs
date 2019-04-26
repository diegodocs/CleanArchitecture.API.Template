using Api.Common.Repository.Contracts.Core.Repository;
using Api.Template.Domain.Tests.Factories;
using Api.Template.Domain.Tests.Models;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Api.Template.Domain.Tests.InjectionModules
{
    public class IoCModuleDomainTest : Module
    {
        public IConfigurationRoot Configuration { get; }

        public IoCModuleDomainTest()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }
        protected override void Load(ContainerBuilder builder)
        {
            // Test Factories
            builder.RegisterType<FundFactory>().AsSelf();            
            builder.RegisterType<UserFactory>().AsSelf();            

            // ASP.NET HttpContext dependency
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            
            //register all IDatabaseMigration in this assembly
            builder.RegisterAssemblyTypes(typeof(BaseDomainTests).Assembly)
                .Where(c => c.IsAssignableTo<IDatabaseMigration>())
                .AsImplementedInterfaces();            
        }
    }
}