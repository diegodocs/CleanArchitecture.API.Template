using Api.Template.ApplicationService.InjectionModules;
using Api.Template.Domain.InjectionModules;
using Api.Template.Domain.Tests.InjectionModules;
using Api.Common.Mock.Infrastructure.InjectionModules;
using Api.Common.Repository.Contracts.Core.Repository;
using Autofac;
using System.Collections.Generic;

namespace Api.Template.Domain.Tests.UnitTests
{
    public class BaseDomainTests
    {
        protected readonly IContainer container;

        protected BaseDomainTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new IoCModuleApplicationService());
            builder.RegisterModule(new IoCModuleMockInfra());
            builder.RegisterModule(new IoCAutoMapperModule());
            builder.RegisterModule(new IoCModuleDomainTest());

            container = builder.Build();
            ApplyDbMigrations();
        }

        private void ApplyDbMigrations()
        {
            var migrations = container.Resolve<IEnumerable<IDatabaseMigration>>();

            foreach (var migration in migrations) migration.Up();
        }
    }
}