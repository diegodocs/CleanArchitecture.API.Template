using AutoMapper;
using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Funds;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.Repository.Contracts.Core.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Api.Template.ApplicationService.Services
{
    public class FundAppService : BaseAppService, IFundAppService
    {        
        private readonly IRepository<Fund> fundRepository;        

        public FundAppService(
            IHttpContextAccessor contextAccessor,
            IMessageBus bus,
            IMapper mapper,            
            IRepository<Fund> fundRepository) : base(contextAccessor, bus, mapper)
        {            
            this.fundRepository = fundRepository;
        }

        public IEnumerable<FundViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<FundViewModel>>(fundRepository.All());
        }

        public IEnumerable<FundViewModel> GetActives()
        {
            return Mapper.Map<IEnumerable<FundViewModel>>(fundRepository.FindList(f => f.IsActive));
        }

        public FundViewModel GetById(Guid id)
        {
            return Mapper.Map<FundViewModel>(fundRepository.FindById(id));
        }

        public FundViewModel Create(CreateFundCommand command)
        {
            command.AuditUserId = this.CurrentUser.Id;

            return Mapper
                .Map<FundViewModel>(
                    MessageBus.DispatchCommandTwoWay<CreateFundCommand, Fund>(command).Result);
        }

        public FundViewModel Update(UpdateFundCommand command)
        {
            command.AuditUserId = this.CurrentUser.Id;

            return Mapper
                .Map<FundViewModel>(
                    MessageBus.DispatchCommandTwoWay<UpdateFundCommand, Fund>(command).Result);
        }

        public void Delete(DeleteFundCommand command)
        {
            command.AuditUserId = this.CurrentUser.Id;

            MessageBus.DispatchCommand(command);
        }
    }
}