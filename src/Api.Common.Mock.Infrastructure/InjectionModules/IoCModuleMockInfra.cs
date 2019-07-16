using Api.Common.Cqrs.Core.Bus;
using Api.Common.Mock.Infrastructure.Bus;
using Api.Common.Mock.Infrastructure.Data.Repositories;
using Api.Common.Repository.Contracts.Core.Repository;
using Autofac;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Module = Autofac.Module;

namespace Api.Common.Mock.Infrastructure.InjectionModules
{
    public class IoCModuleMockInfra : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // ASP.NET HttpContext dependency
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            // Service Bus
            builder.RegisterType<InMemoryMessageBus>().As<IMessageBus>();

            // Infra - Data
            builder.RegisterType<InMemoryUnitOfWork>().As<IUnitOfWork>();

            builder
                .RegisterGeneric(typeof(InMemoryRepositorySoftDelete<>))
                .AsImplementedInterfaces()
                .SingleInstance();

            var assemblyToScan = Assembly.GetAssembly(typeof(InMemoryMessageBus));
            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(c => c.IsClass
                            && c.IsInNamespace("Api.Common.Mock.Infrastructure.Services")).AsImplementedInterfaces();
        }
    }
}