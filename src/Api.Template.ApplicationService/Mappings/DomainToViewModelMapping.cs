using AutoMapper;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Models;

namespace Api.Template.ApplicationService.Mappings
{
    public class DomainToViewModelMapping : Profile
    {
        public DomainToViewModelMapping()
        {
            CreateMap<Fund, FundViewModel>();           
            CreateMap<User, UserViewModel>();
        }
    }
}