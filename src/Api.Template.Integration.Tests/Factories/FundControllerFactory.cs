using FluentAssertions;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Funds;
using Api.Common.WebServer.Server;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.Template.Integration.Tests.Factories
{
    public class FundControllerFactory
    {
        private readonly HttpClient client;
        private const string url = "/api/fund";

        public FundControllerFactory(HttpClient client)
        {
            this.client = client;
        }

        public async Task<FundViewModel> Create(string name, string description, string legalName)
        {            
            //Act
            var responseModel = await Create(new CreateFundCommand(name, description, legalName));
            var viewModel = JsonConvert.DeserializeObject<FundViewModel>(responseModel.Result.ToString());

            // Assert
            responseModel.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModel.Should().BeOfType<FundViewModel>();

            viewModel.Id.Should().NotBeEmpty();
            viewModel.Name.Should().Be(name);
            viewModel.Description.Should().Be(description);

            return viewModel;
        }

        public async Task<FundViewModel> Create()
        {
            var name = $"Fund";
            var description = "Fund L.P.";
            var legalName = $"Fund L.P.";

            return await Create(name, description, legalName);
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

        public async Task<ApiResponse> Create(CreateFundCommand command)
        {
            // Arrange
            var requestBody = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, requestBody);

            // Assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
            apiResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            apiResponse.ResponseException.Should().BeNull();
            apiResponse.Message.Should().Be("Request successful.");

            return apiResponse;
        }

        public async Task<FundViewModel> Update(FundViewModel viewModel)
        {
            // Arrange           
            viewModel.Name = $"{viewModel.Name}-{DateTime.UtcNow.ToLongTimeString()}";
            viewModel.Description = $"{viewModel.Description}-{DateTime.UtcNow.ToLongTimeString()}";
            viewModel.LegalName = $"{viewModel.LegalName}-{DateTime.UtcNow.ToLongTimeString()}";

            var command = new UpdateFundCommand(viewModel.Id, viewModel.Name, viewModel.Description, viewModel.LegalName);
            var requestBody = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync($"{url}/{viewModel.Id}", requestBody);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
            var viewModelResponse = JsonConvert.DeserializeObject<FundViewModel>(apiResponse.Result.ToString());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            apiResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            viewModelResponse.Should().BeOfType<FundViewModel>();

            viewModelResponse.Id.Should().NotBeEmpty();
            viewModelResponse.Id.Should().Be(viewModel.Id);
            viewModelResponse.Name.Should().Be(viewModel.Name);
            viewModelResponse.Description.Should().Be(viewModel.Description);

            return viewModelResponse;
        }
    }
}