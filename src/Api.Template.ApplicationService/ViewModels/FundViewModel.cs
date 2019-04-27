using System;
using Api.Common.Repository.Contracts.Core.Entities;

namespace Api.Template.ApplicationService.ViewModels
{
    public class FundViewModel : IApplicationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}