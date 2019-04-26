using Autofac;
using Api.Template.Domain.Models;

namespace Api.Template.ApplicationService.InjectionModules
{
    public class IoCModuleDomain : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyToScan = System.Reflection.Assembly.GetAssembly(typeof(Fund));

            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(c => c.IsClass
                    && c.IsInNamespace("Api.Template.Domain.CommandHandlers")).AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(c => c.IsClass
                    && c.IsInNamespace("Api.Template.Domain.Commands")).AsImplementedInterfaces();
        }
    }
}