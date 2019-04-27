using Autofac;
using FluentAssertions;
using Api.Template.Domain.Commands.Funds;
using Api.Template.Domain.Tests.Factories;
using Api.Common.Repository.Contracts.Core.Exceptions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Api.Template.Domain.Tests.Models
{
    [TestFixture]
    public class FundDomainTests : BaseDomainTests
    {
        private readonly FundFactory fundFactory;

        public FundDomainTests()
        {
            fundFactory = container.Resolve<FundFactory>();
        }

        [Test]
        public void WhenCreateNewEmptyFund_Then_Error()
        {
            //arrange
            var name = "";
            var description = "";
            
            var command = new CreateFundCommand(name, description);

            //act
            Action action = () => { fundFactory.Create(command); };

            //assert
            action.Should()
                .Throw<ModelException>()
                .WithMessage(
                    "This object instance is not valid based on DataAnnotation definitions. See more details on Errors list.");
        }

        [Test]
        public void WhenCreateNewFund_Then_ICanFindItById()
        {
            //act
            var responseCreate = fundFactory.Create();
            var responseFindById = fundFactory.GetById(responseCreate.Id);

            //assert
            responseFindById.Id.Should().Be(responseCreate.Id);
            responseFindById.Name.Should().Be(responseCreate.Name);
            responseFindById.Description.Should().Be(responseCreate.Description);
        }

        [Test]
        public void WhenCreateNewFundAndUpdate_Then_ICanFindItById()
        {
            //arrange
            var expectedNameAfterUpdate = $"AfterUpdate-Fund-Test-{DateTime.UtcNow.ToLongTimeString()}";
            var expectedDescriptionAfterUpdate = $"AfterUpdate-Description-Test-{DateTime.UtcNow.ToLongTimeString()}";
            
            //act
            var responseCreate = fundFactory.Create();
            var commandUpdate = new UpdateFundCommand(
                responseCreate.Id,
                expectedNameAfterUpdate,
                expectedDescriptionAfterUpdate);

            var responseUpdate = fundFactory.Update(commandUpdate);
            var responseFindById = fundFactory.GetById(responseCreate.Id);

            //assert
            responseFindById.Id.Should().Be(responseUpdate.Id);
            responseFindById.Name.Should().Be(responseUpdate.Name);
            responseFindById.Description.Should().Be(responseUpdate.Description);
        }

        [Test]
        public void WhenCreateNewFundAndUpdateAndDelete_Then_Success()
        {
            //arrange
            var expectedNameAfterUpdate = $"AfterUpdate-Fund-Test-{DateTime.UtcNow.ToLongTimeString()}";
            var expectedDescriptionAfterUpdate = $"AfterUpdate-Description-Test-{DateTime.UtcNow.ToLongTimeString()}";                        
            
            //act
            var responseCreate = fundFactory.Create();
            var commandUpdate = new UpdateFundCommand(
                responseCreate.Id,
                expectedNameAfterUpdate,
                expectedDescriptionAfterUpdate);

            fundFactory.Update(commandUpdate);
            fundFactory.Delete(responseCreate.Id);

            var responseFindById = fundFactory.GetById(responseCreate.Id);

            //assert
            responseFindById.Should().BeNull();
        }

        [Test]
        public void WhenCreateNewFundWithNoDescription_Then_ICanNotFindItById()
        {
            //arrange
            var name = $"Fund L.P.";
            var description = $"";

            var command = new CreateFundCommand(name, description);

            //act
            Action action = () => { fundFactory.Create(command); };

            //assert
            action.Should()
                .Throw<ModelException>()
                .WithMessage(
                    "This object instance is not valid based on DataAnnotation definitions. See more details on Errors list.");
        }

        [Test]
        public void WhenCreateNewFundWithNoName_Then_Error()
        {
            //arrange
            var expectedNumberOfErrors = 1;

            var name = $"";
            var description = $"Fund L.P.";          

            var command = new CreateFundCommand(name, description);

            //act
            Action action = () => { fundFactory.Create(command); };

            //assert
            action.Should()
                .Throw<ModelException>()
                .Where(x => x.Errors.Count() == expectedNumberOfErrors);
        }

        [Test]
        public void WhenCreateTwoFundsAndDeleteOne_Then_ReturnAllFundsGreaterThenActiveFunds()
        {
            //act
            var responseCreateFirst = fundFactory.Create();
            var responseCreateSecond = fundFactory.Create();

            fundFactory.Delete(responseCreateFirst.Id);

            var allFunds = fundFactory.GetAll();
            var activeFunds = fundFactory.GetActives();

            //assert
            allFunds.Should().HaveCountGreaterThan(activeFunds.Count());
        }
    }
}