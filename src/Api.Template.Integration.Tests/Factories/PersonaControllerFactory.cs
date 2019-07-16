using Api.Common.WebServer.Server;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Personas;
using Api.Template.Integration.Tests.Factories.Interface;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.Template.Integration.Tests.Factories
{
    public class PersonaControllerFactory : IBaseIntegrationTestFactory
    {
        private const string url = "/api/v1/Persona";
        private readonly HttpClient client;

        public PersonaControllerFactory(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PersonaViewModel> Create()
        {
            var name = "Student";

            //Act
            var responseModel = await Create(new CreatePersonaCommand(name));
            var viewModel =
                JsonConvert.DeserializeObject<PersonaViewModel>(responseModel.Result.ToString());

            // Assert
            responseModel.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModel.Should().BeOfType<PersonaViewModel>();

            viewModel.Id.Should().NotBeEmpty();
            viewModel.Name.Should().Be(name);

            return viewModel;
        }

        public async Task Delete(Guid id)
        {
            // Act
            var response = await client.DeleteAsync($"{url}/{id}");
            var responseModel = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseModel.StatusCode.Should().Be((int)HttpStatusCode.OK);
            responseModel.Result.Should().Be("");
        }

        public async Task<ApiResponse> Get(Guid id)
        {
            // Act
            var response = await client.GetAsync($"{url}/{id}");
            var responseModel = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

            return responseModel;
        }

        private async Task<ApiResponse> Create(CreatePersonaCommand command)
        {
            // Arrange
            var requestBody =
                new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, requestBody);

            // Assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
            apiResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            apiResponse.ResponseException.Should().BeNull();
            apiResponse.Message.Should().Be("Request successful.");

            return apiResponse;
        }

        public async Task<PersonaViewModel> Update(PersonaViewModel viewModel)
        {
            // Arrange
            viewModel.Name = $"{viewModel.Name}-{DateTime.UtcNow.ToLongTimeString()}";

            var command = new UpdatePersonaCommand(viewModel.Id, viewModel.Name);
            var requestBody =
                new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync($"{url}/{viewModel.Id}", requestBody);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
            var viewModelResponse =
                JsonConvert.DeserializeObject<PersonaViewModel>(apiResponse.Result.ToString());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            apiResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModelResponse.Should().BeOfType<PersonaViewModel>();

            viewModelResponse.Id.Should().NotBeEmpty();
            viewModelResponse.Id.Should().Be(viewModel.Id);
            viewModelResponse.Name.Should().Be(viewModel.Name);

            return viewModelResponse;
        }
    }
}