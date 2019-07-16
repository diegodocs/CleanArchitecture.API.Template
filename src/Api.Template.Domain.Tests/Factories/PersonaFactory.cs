using Api.Common.Repository.Contracts.Core.Validations;
using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Personas;
using Api.Template.Domain.Tests.Factories.Interface;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace Api.Template.Domain.Tests.Factories
{
    public class PersonaFactory : IBaseDomainTestFactory
    {
        private readonly IPersonaAppService appService;

        public PersonaFactory(IPersonaAppService appService)
        {
            this.appService = appService;
        }

        public PersonaViewModel Create()
        {
            var name = "Rascunho";

            var command = new CreatePersonaCommand(name);
            return Create(command);
        }

        public void Delete(Guid id)
        {
            var commandDelete = new DeletePersonaCommand(id);
            appService.Delete(commandDelete);
        }

        public PersonaViewModel Get(Guid id)
        {
            return appService.Get(id);
        }

        public IEnumerable<PersonaViewModel> GetAll()
        {
            return appService.GetAll();
        }

        public IEnumerable<PersonaViewModel> GetListByName(string name)
        {
            return appService.GetListByName(name);
        }

        public PersonaViewModel Create(CreatePersonaCommand command)
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

        public PersonaViewModel Update(UpdatePersonaCommand command)
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