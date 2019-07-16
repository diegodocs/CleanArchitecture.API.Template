using Api.Common.Core.Authentication;
using Api.Common.Repository.Contracts.Core.Repository;
using Api.Template.Domain.Tests.Factories.Interface;
using Api.Template.Domain.Tests.UnitTests;
using Autofac;

namespace Api.Template.Domain.Tests.InjectionModules
{
    public class IoCModuleDomainTest : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Test Factories
            builder.RegisterAssemblyTypes(typeof(BaseDomainTests).Assembly)
                .Where(c => c.IsAssignableTo<IBaseDomainTestFactory>())
                .AsSelf();

            var signingConfigurations = new SigningConfigurations();
            builder.RegisterInstance(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            builder.RegisterInstance(tokenConfigurations);

            //register all IDatabaseMigration in this assembly
            builder.RegisterAssemblyTypes(typeof(BaseDomainTests).Assembly)
                .Where(c => c.IsAssignableTo<IDatabaseMigration>())
                .AsImplementedInterfaces();
        }
    }
}