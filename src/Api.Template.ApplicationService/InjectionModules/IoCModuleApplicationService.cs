using Autofac;
using Api.Template.ApplicationService.InjectionModules;
using Api.Template.ApplicationService.Services;
using Api.Template.Domain.Models;
using Api.Common.WebServer.Server;

namespace Api.Template.Domain.InjectionModules
{
    public class IoCModuleApplicationService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Domain Modules: Command and CommandHandlers
            builder.RegisterModule<IoCModuleDomain>();

            var assemblyToScan = System.Reflection.Assembly.GetAssembly(typeof(BaseAppService));

            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(c => c.IsClass
                    && c.IsInNamespace("Api.Template.ApplicationService.Services")).AsImplementedInterfaces();
                        
            builder.RegisterType<UserContext>().AsSelf();
        }
    }
}