using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.ReleaseCallsStatus;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.Repository.Contracts.Core.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Template.ApplicationService.Services
{
    public class ReleaseCallStatusAppService : BaseAppService, IReleaseCallStatusAppService
    {
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;
        private readonly IRepository<ReleaseCallStatus> repository;

        public ReleaseCallStatusAppService(
            IHttpContextAccessor contextAccessor,
            IMessageBus messageBus,
            IMapper mapper,
            IRepository<ReleaseCallStatus> repository) : base(contextAccessor)
        {
            this.messageBus = messageBus;
            this.mapper = mapper;
            this.repository = repository;
        }

        public IEnumerable<ReleaseCallStatusViewModel> GetAll()
        {
            return mapper.Map<IEnumerable<ReleaseCallStatusViewModel>>(repository.All()).OrderBy(x => x.Name);
        }

        public ReleaseCallStatusViewModel Get(Guid id)
        {
            return mapper.Map<ReleaseCallStatusViewModel>(repository.FindById(id));
        }

        public IEnumerable<ReleaseCallStatusViewModel> GetListByName(string name)
        {
            return
                mapper.Map<IEnumerable<ReleaseCallStatusViewModel>>(
                        repository
                            .FindList(x => x.Name.Contains(name)))
                    .OrderBy(x => x.Name);
        }

        public ReleaseCallStatusViewModel GetById(Guid id)
        {
            return mapper.Map<ReleaseCallStatusViewModel>(repository.FindById(id));
        }

        public ReleaseCallStatusViewModel Create(CreateReleaseCallStatusCommand command)
        {
            return mapper
                .Map<ReleaseCallStatusViewModel>(
                    messageBus
                        .DispatchCommandTwoWay<CreateReleaseCallStatusCommand, ReleaseCallStatus
                        >(command).Result);
        }

        public ReleaseCallStatusViewModel Update(UpdateReleaseCallStatusCommand command)
        {
            return mapper
                .Map<ReleaseCallStatusViewModel>(
                    messageBus
                        .DispatchCommandTwoWay<UpdateReleaseCallStatusCommand, ReleaseCallStatus
                        >(command).Result);
        }

        public void Delete(DeleteReleaseCallStatusCommand commandDelete)
        {
            messageBus.DispatchCommand(commandDelete);
        }
    }
}