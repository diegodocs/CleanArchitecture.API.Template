using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Integration.Tests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Api.Template.Integration.Tests.Controllers
{
    [TestFixture]
    public class FundControllerTest : ApiControllerTest
    {
        private readonly FundControllerFactory factory;

        public FundControllerTest()
        {
            factory = new FundControllerFactory(client);
        }

        [Test]
        public async Task WhenCreateFund_Then_ICanFindItById()
        {
            // Act
            var viewModelCreate = await factory.Create();
            var responseGet = await factory.Get(viewModelCreate.Id);

            var viewModelGet = JsonConvert.DeserializeObject<FundViewModel>(responseGet.Result.ToString());

            // Assert
            responseGet.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModelGet.Should().BeOfType<FundViewModel>();
            viewModelGet.Id.Should().NotBeEmpty();
            viewModelGet.Id.Should().Be(viewModelCreate.Id);
            viewModelGet.Name.Should().Be(viewModelCreate.Name);
        }

        [Test]
        public async Task WhenCreateFundAndDelete_Then_Deleted()
        {
            // Act
            var viewModelCreate = await factory.Create();
            await factory.Delete(viewModelCreate.Id);
            var responseGet = await factory.Get(viewModelCreate.Id);

            // Assert
            responseGet.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Test]
        public async Task WhenCreateFundAndUpdate_Then_ICanFindItById()
        {
            // Act
            var viewModelCreate = await factory.Create();
            var viewModelUpdate = await factory.Update(viewModelCreate);
            var responseGet = await factory.Get(viewModelCreate.Id);
            var viewModelGet = JsonConvert.DeserializeObject<FundViewModel>(responseGet.Result.ToString());

            // Assert
            responseGet.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModelGet.Should().BeOfType<FundViewModel>();

            viewModelGet.Id.Should().Be(viewModelUpdate.Id);
            viewModelGet.Name.Should().Be(viewModelUpdate.Name);
            viewModelGet.Description.Should().Be(viewModelUpdate.Description);
        }


        [Test]
        public async Task WhenCreateNewFundAndUpdateAndDelete_Then_Success()
        {
            // Act
            var viewModelCreate = await factory.Create();
            var viewModelUpdate = await factory.Update(viewModelCreate);

            var responseGet = await factory.Get(viewModelCreate.Id);
            var viewModelGet = JsonConvert.DeserializeObject<FundViewModel>(responseGet.Result.ToString());

            await factory.Delete(viewModelCreate.Id);
            var responseGetAfterDelete = await factory.Get(viewModelCreate.Id);

            // Assert

            viewModelGet.Id.Should().Be(viewModelUpdate.Id);
            viewModelGet.Name.Should().Be(viewModelUpdate.Name);
            viewModelGet.Description.Should().Be(viewModelUpdate.Description);

            responseGetAfterDelete.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
    }
}