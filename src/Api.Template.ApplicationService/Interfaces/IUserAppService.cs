using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.User;
using System;
using System.Collections.Generic;

namespace Api.Template.ApplicationService.Interfaces
{
    public interface IUserAppService : IBaseAppService
    {
        IEnumerable<UserViewModel> GetApproverUsers();
        UserViewModel GetById(Guid id);
        UserViewModel Login(LoginUserCommand command);
    }
}