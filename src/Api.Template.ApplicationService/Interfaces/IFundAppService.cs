using System;
using System.Collections.Generic;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Funds;

namespace Api.Template.ApplicationService.Interfaces
{
    public interface IFundAppService : IBaseAppService
    {
        FundViewModel Create(CreateFundCommand command);

        FundViewModel Update(UpdateFundCommand command);

        void Delete(DeleteFundCommand command);

        IEnumerable<FundViewModel> GetAll();

        IEnumerable<FundViewModel> GetActives();

        FundViewModel GetById(Guid id);
    }
}