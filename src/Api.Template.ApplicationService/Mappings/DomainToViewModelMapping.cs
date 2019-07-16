using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Models;
using AutoMapper;

namespace Api.Template.ApplicationService.Mappings
{
    public class DomainToViewModelMapping : Profile
    {
        public DomainToViewModelMapping()
        {
            CreateMap<Persona, PersonaViewModel>();
        }
    }
}