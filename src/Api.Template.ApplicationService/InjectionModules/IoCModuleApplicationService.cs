using Api.Template.ApplicationService.Services;
using Api.Common.WebServer.Server;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace Api.Template.Domain.InjectionModules
{
    public class IoCModuleApplicationService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Domain Modules: Command and CommandHandlers
            builder.RegisterModule<IoCModuleDomain>();

            var assemblyToScan = Assembly.GetAssembly(typeof(BaseAppService));

            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(c => c.IsClass
                            && c.IsInNamespace("Api.Template.ApplicationService.Services")).AsImplementedInterfaces();

            builder.RegisterType<UserContext>().AsSelf();
        }
    }
}