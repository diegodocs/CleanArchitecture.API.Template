using Api.Common.Cqrs.Core.Bus;
using Api.Common.Mock.Infrastructure.Bus;
using Api.Common.Repository.EFCore;
using Api.Template.DbEFCore.Repositories;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Api.Template.Infrastructure.InjectionModules
{
    public class IoCModuleInfrastructure : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // ASP.NET HttpContext dependency
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            // Service Bus
            builder.RegisterType<InMemoryMessageBus>().As<IMessageBus>();

            // Infra - DbContext            
            builder.RegisterType<EfCoreDbContext>().As<DbContext>();           

            // Infra - Repository
            builder
                .RegisterGeneric(typeof(EfCoreRepositorySoftDelete<>))
                .AsImplementedInterfaces();                       
        }
    }   
}