using System;
using System.Collections.Generic;
using FluentAssertions;
using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Funds;
using Api.Template.Domain.Models;
using Api.Common.Repository.Contracts.Core.Validations;

namespace Api.Template.Domain.Tests.Factories
{
    public class FundFactory 
    {
        private readonly IFundAppService fundAppService;

        public FundFactory(IFundAppService fundAppService)
        {
            this.fundAppService = fundAppService;
            this.fundAppService.CurrentUser = new Common.WebServer.Server.UserContext { Id = new UserDefinition().SystemAppUser.Id };
        }

        public FundViewModel Create()
        {
            var fund = new Fund
            {
                AuditUserId = new Guid("9ae42a23-1e01-4402-bc3f-c74a8ee295c6"),
                CreateDate = DateTime.UtcNow,
                Description = "2016 Fund L.P.",               
                Id = Guid.NewGuid(),
                IsActive = true,
                Name = " Fund"                
            };

            return Create(fund);
        }

        public FundViewModel Create(Fund fund)
        {
            var command = new CreateFundCommand(fund.Name, fund.Description);
            return Create(command);
        }

        public void Delete(Guid id)
        {
            var commandDelete = new DeleteFundCommand(id);
            fundAppService.Delete(commandDelete);
        }

        public FundViewModel GetById(Guid id)
        {
            return fundAppService.GetById(id);
        }

        public IEnumerable<FundViewModel> GetAll()
        {
            return fundAppService.GetAll();
        }

        public IEnumerable<FundViewModel> GetActives()
        {
            return fundAppService.GetActives();
        }

        public FundViewModel Create(CreateFundCommand command)
        {
            //arrange
            const int expectedNumberOfErrors = 0;

            //act
            var response = fundAppService.Create(command);

            //assert
            command.ValidateModelAnnotations().Count.Should().Be(expectedNumberOfErrors);
            response.Id.Should().NotBe(Guid.Empty);
            response.Name.Should().Be(command.Name);
            response.Description.Should().Be(command.Description);

            return response;
        }

        public FundViewModel Update(UpdateFundCommand command)
        {
            //arrange
            const int expectedNumberOfErrors = 0;

            //act
            var response = fundAppService.Update(command);

            //assert
            command.ValidateModelAnnotations().Count.Should().Be(expectedNumberOfErrors);
            response.Id.Should().Be(command.Id);
            response.Name.Should().Be(command.Name);
            response.Description.Should().Be(command.Description);

            return response;
        }
    }
}