using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.ReleaseCallsStatus;
using Api.Template.Domain.Tests.Factories.Interface;
using Api.Common.Repository.Contracts.Core.Validations;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace Api.Template.Domain.Tests.Factories
{
    public class ReleaseCallStatusFactory : IBaseDomainTestFactory
    {
        private readonly IReleaseCallStatusAppService appService;

        public ReleaseCallStatusFactory(IReleaseCallStatusAppService appService)
        {
            this.appService = appService;
        }

        public ReleaseCallStatusViewModel Create()
        {
            var name = "Rascunho";

            var command = new CreateReleaseCallStatusCommand(name);
            return Create(command);
        }

        public void Delete(Guid id)
        {
            var commandDelete = new DeleteReleaseCallStatusCommand(id);
            appService.Delete(commandDelete);
        }

        public ReleaseCallStatusViewModel Get(Guid id)
        {
            return appService.Get(id);
        }

        public IEnumerable<ReleaseCallStatusViewModel> GetAll()
        {
            return appService.GetAll();
        }

        public IEnumerable<ReleaseCallStatusViewModel> GetListByName(string name)
        {
            return appService.GetListByName(name);
        }

        public ReleaseCallStatusViewModel Create(CreateReleaseCallStatusCommand command)
        {
            //arrange
            const int expectedNumberOfErrors = 0;

            //act
            var response = appService.Create(command);

            //assert
            command.ValidateModelAnnotations().Count.Should().Be(expectedNumberOfErrors);
            response.Id.Should().NotBe(Guid.Empty);
            response.Name.Should().Be(command.Name);

            return response;
        }

        public ReleaseCallStatusViewModel Update(UpdateReleaseCallStatusCommand command)
        {
            //arrange
            const int expectedNumberOfErrors = 0;

            //act
            var response = appService.Update(command);

            //assert
            command.ValidateModelAnnotations().Count.Should().Be(expectedNumberOfErrors);
            response.Id.Should().Be(command.Id);
            response.Name.Should().Be(command.Name);

            return response;
        }
    }
}