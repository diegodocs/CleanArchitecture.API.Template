using Api.Template.Domain.Models;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace Api.Template.Domain.InjectionModules
{
    public class IoCModuleDomain : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyToScan = Assembly.GetAssembly(typeof(UserDefinition));

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