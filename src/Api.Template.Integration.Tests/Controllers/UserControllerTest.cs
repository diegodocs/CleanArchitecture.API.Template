using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Models;
using Api.Template.Integration.Tests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Api.Template.Integration.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest : ApiControllerTest
    {
        private readonly UserControllerFactory factory;

        public UserControllerTest()
        {
            factory = new UserControllerFactory(client);
        }

        [Test]
        public async Task WhenLoginUser_Then_ICanSeeUserInformation()
        {
            //arrange
            var now = DateTime.UtcNow;
            var name = "User Name";
            var login = "usertest01";
            var email = "usertest01@company.com";


            //act
            var response = await factory.Login(name, login, email);
            var viewModel = JsonConvert.DeserializeObject<UserViewModel>(response.Result.ToString());

            // Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModel.Should().BeOfType<UserViewModel>();

            viewModel.Name.Should().Be(name);
            viewModel.Login.Should().Be(login);
            viewModel.Email.Should().Be(email);
        }

        [TestCase("User Name", "usertest01", "usertest01@company.com")]
        public async Task WhenLoginUser_Then_ICanGetById(string name, string login, string email)
        {
            //arrange
            var now =DateTime.UtcNow;
           
            //act
            var response = await factory.Login(name, login, email);
            var viewModel = JsonConvert.DeserializeObject<UserViewModel>(response.Result.ToString());

            var responseGet = await factory.GetById(viewModel.Id);
            var viewModelGet = JsonConvert.DeserializeObject<UserViewModel>(responseGet.Result.ToString());

            // Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModel.Should().BeOfType<UserViewModel>();

            responseGet.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModelGet.Should().BeOfType<UserViewModel>();

            viewModel.Id.Should().Be(viewModelGet.Id);
            viewModel.Name.Should().Be(viewModelGet.Name);
            viewModel.Login.Should().Be(viewModelGet.Login);
            viewModel.Email.Should().Be(viewModelGet.Email);
        }
    }
}