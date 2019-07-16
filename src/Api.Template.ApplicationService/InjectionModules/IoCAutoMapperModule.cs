using Api.Template.ApplicationService.Mappings;
using Autofac;
using AutoMapper;

namespace Api.Template.ApplicationService.InjectionModules
{
    public class IoCAutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(
                    context => new MapperConfiguration(cfg => { cfg.AddProfile(new DomainToViewModelMapping()); }))
                .AsSelf().SingleInstance();

            builder.Register(
                    c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}