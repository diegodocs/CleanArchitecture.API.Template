using Api.Template.Integration.Tests.Factories.Interface;
using Api.Template.Integration.Tests.IntegrationTests;
using Autofac;

namespace Api.Template.Integration.Tests.InjectionModules
{
    public class IoCModuleIntegrationTest : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseControllerTest).Assembly)
                .Where(c => c.IsAssignableTo<IBaseIntegrationTestFactory>())
                .AsSelf();
        }
    }
}