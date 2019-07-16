using Api.Common.Repository.Contracts.Core.Exceptions;
using Api.Template.Domain.Commands.Personas;
using Api.Template.Domain.Tests.Factories;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Api.Template.Domain.Tests.UnitTests
{
    [TestFixture]
    public class PersonaDomainTests : BaseDomainTests
    {
        private readonly PersonaFactory factory;

        public PersonaDomainTests()
        {
            factory = container.Resolve<PersonaFactory>();
        }

        [Test]
        public void WhenCreate_Then_ICanFindItById()
        {
            //act
            var responseCreate = factory.Create();
            var responseFindById = factory.Get(responseCreate.Id);

            //assert
            responseFindById.Id.Should().Be(responseCreate.Id);
            responseFindById.Name.Should().Be(responseCreate.Name);
        }

        [Test]
        public void WhenCreateAndUpdate_Then_ICanFindItById()
        {
            //arrange
            var expectedNameAfterUpdate =
                $"AfterUpdate-Persona-Test-{DateTime.UtcNow.ToLongTimeString()}";

            //act
            var responseCreate = factory.Create();
            var commandUpdate = new UpdatePersonaCommand(
                responseCreate.Id,
                expectedNameAfterUpdate);

            var responseUpdate = factory.Update(commandUpdate);
            var responseFindById = factory.Get(responseCreate.Id);

            //assert
            responseFindById.Id.Should().Be(responseUpdate.Id);
            responseFindById.Name.Should().Be(responseUpdate.Name);
        }

        [Test]
        public void WhenCreateAndUpdateAndDelete_Then_Success()
        {
            //arrange
            var expectedNameAfterUpdate =
                $"AfterUpdate-Persona-Test-{DateTime.UtcNow.ToLongTimeString()}";

            //act
            var responseCreate = factory.Create();
            var commandUpdate = new UpdatePersonaCommand(
                responseCreate.Id,
                expectedNameAfterUpdate);

            factory.Update(commandUpdate);
            factory.Delete(responseCreate.Id);

            var responseFindById = factory.Get(responseCreate.Id);

            //assert
            responseFindById.Should().BeNull();
        }

        [Test]
        public void WhenCreateNewEmpty_Then_Error()
        {
            //arrange
            var name = "";

            var command = new CreatePersonaCommand(name);

            //act
            Action action = () => { factory.Create(command); };

            //assert
            action.Should()
                .Throw<ModelException>()
                .WithMessage(
                    "This object instance is not valid based on DataAnnotation definitions. See more details on Errors list.");
        }

        [Test]
        public void WhenCreateWithNoName_Then_Error()
        {
            //arrange
            var expectedNumberOfErrors = 1;

            var name = "";

            var command = new CreatePersonaCommand(name);

            //act
            Action action = () => { factory.Create(command); };

            //assert
            action.Should()
                .Throw<ModelException>()
                .Where(x => x.Errors.Count() == expectedNumberOfErrors);
        }
    }
}