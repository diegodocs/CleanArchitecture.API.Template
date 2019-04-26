using Autofac;
using Api.Template.Integration.Tests.Factories;

namespace Api.Template.Integration.Tests.InjectionModules
{
    public class IoCModuleIntegrationTest : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FundControllerFactory>().AsSelf();          
        }
    }
}