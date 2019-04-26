using Autofac;
using FluentAssertions;
using Api.Template.Domain.Tests.Factories;
using NUnit.Framework;
using System;

namespace Api.Template.Domain.Tests.Models
{
    [TestFixture]
    public class UserDomainTests : BaseDomainTests
    {
        private readonly UserFactory factory;

        public UserDomainTests()
        {
            factory = container.Resolve<UserFactory>();
        }

        [Test]
        public void WhenLoginUser_Then_ICanSeeUserInformation()
        {
            //arrange
            var now = DateTime.UtcNow;
            var name = "User Test 1";
            var login = "userTest1";
            var email = "userTest1@company.com";

            //act
            var response = factory.Login(name, login, email);

            //assert
            response.Name.Should().Be(name);
            response.Login.Should().Be(login.ToLower());
            response.Email.Should().Be(email.ToLower());
        }

        [TestCase("User Test 1", "userTest1", "userTest1@company.com")]
        public void WhenLoginUser_Then_ICanGetById(string name, string login, string email)
        {
            //arrange
            var now = DateTime.UtcNow;
            //act
            var createResponse = factory.Login(name, login, email);
            var getResponse = factory.GetById(createResponse.Id);

            //assert
            createResponse.Id.Should().Be(getResponse.Id);
            createResponse.Name.Should().Be(getResponse.Name);
            createResponse.Login.Should().Be(getResponse.Login);
            createResponse.Email.Should().Be(getResponse.Email);
        }

        [TestCase("User Test 1", "userTest1", "userTest1@company.com")]
        public void WhenLoginNewUser_Then_ICanGetById(string name, string login, string email)
        {
            //arrange
            var now = DateTime.UtcNow;
            //act
            var createResponse = factory.Login(name, login, email);
            var getResponse = factory.GetById(createResponse.Id);

            //assert
            createResponse.Id.Should().Be(getResponse.Id);
            createResponse.Name.Should().Be(getResponse.Name);
            createResponse.Login.Should().Be(getResponse.Login);
            createResponse.Email.Should().Be(getResponse.Email);
        }
    }
}