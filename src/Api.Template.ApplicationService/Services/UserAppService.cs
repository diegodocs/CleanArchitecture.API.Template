using AutoMapper;
using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.User;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.Repository.Contracts.Core.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Api.Template.ApplicationService.Services
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IRepository<User> userRepository;

        public UserAppService(
            IHttpContextAccessor contextAccessor,
            IMessageBus bus,
            IMapper mapper,
            IRepository<User> userRepository) : base(contextAccessor, bus, mapper)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<UserViewModel> GetApproverUsers()
        {
            var approvers = userRepository.FindList(u => u.Id != CurrentUser.Id && u.IsApprover);
            return Mapper.Map<IEnumerable<UserViewModel>>(approvers);
        }

        public UserViewModel GetById(Guid id)
        {
            return Mapper.Map<UserViewModel>(userRepository.FindById(id));
        }

        public UserViewModel Login(LoginUserCommand command)
        {
            return Mapper.Map<UserViewModel>(MessageBus.DispatchCommandTwoWay<LoginUserCommand, User>(command).Result);
        }
    }
}