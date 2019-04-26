using Api.Common.WebServer.Server;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.Template.Integration.Tests.Factories
{
    public class UserControllerFactory
    {
        private readonly HttpClient client;

        public UserControllerFactory(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse> Login(string name, string login, string email)
        {
            // Arrange
            var requestBody =
                new StringContent($"{{'Name':'{name}', 'Login': '{login}', 'Email':'{email}' }}", Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/user/login", requestBody);
            return JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse> GetById(Guid id)
        {
            // Act
            var response = await client.GetAsync($"/api/user/{id}");
            var responseModel = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

            return responseModel;
        }

        public async Task<ApiResponse> GetApprovers()
        {
            // Act
            var response = await client.GetAsync($"/api/user/approvers");
            var responseModel = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

            return responseModel;
        }
    }
}