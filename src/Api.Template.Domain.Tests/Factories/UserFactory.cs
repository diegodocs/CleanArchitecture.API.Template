using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.User;
using Api.Template.Domain.Models;
using System;
using System.Collections.Generic;

namespace Api.Template.Domain.Tests.Factories
{
    public class UserFactory
    {
        private readonly IUserAppService userAppService;

        public UserFactory(IUserAppService userAppService)
        {
            this.userAppService = userAppService;
            this.userAppService.CurrentUser = new Common.WebServer.Server.UserContext { Id = new UserDefinition().SystemAppUser.Id };
        }

        public UserViewModel Login(string name, string login, string email)
        {
            var command = new LoginUserCommand(name, login, email, null, false, false);
            return userAppService.Login(command);
        }

        public UserViewModel GetById(Guid userId)
        {
            return userAppService.GetById(userId);
        }
    }
}