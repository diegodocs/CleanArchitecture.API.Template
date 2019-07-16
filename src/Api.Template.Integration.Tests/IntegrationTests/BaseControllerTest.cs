using Api.Template.Integration.Tests.Server;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Api.Template.Integration.Tests.IntegrationTests
{
    [SetUpFixture]
    public class BaseControllerTest
    {
        public static HttpClient Client;
        private static TestServer server;

        [OneTimeSetUp]
        public void RunBeforeAllTests()
        {
            // Arrange
            server = new TestServer(new WebHostBuilder()
                .ConfigureServices(s => s.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<StartupIntegrationTest>());

            Client = server.CreateClient();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            Client.Dispose();
            server.Dispose();
        }
    }
}