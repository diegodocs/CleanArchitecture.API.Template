using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.ReleaseCallsStatus;
using System;
using System.Collections.Generic;

namespace Api.Template.ApplicationService.Interfaces
{
    public interface IReleaseCallStatusAppService
    {
        IEnumerable<ReleaseCallStatusViewModel> GetAll();

        void Delete(DeleteReleaseCallStatusCommand commandDelete);

        ReleaseCallStatusViewModel Get(Guid id);

        IEnumerable<ReleaseCallStatusViewModel> GetListByName(string name);

        ReleaseCallStatusViewModel Create(CreateReleaseCallStatusCommand command);

        ReleaseCallStatusViewModel Update(UpdateReleaseCallStatusCommand command);
    }
}