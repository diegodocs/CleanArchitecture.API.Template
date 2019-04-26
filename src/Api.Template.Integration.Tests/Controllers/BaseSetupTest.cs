using System.IO;
using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using Api.Template.Integration.Tests.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Api.Template.Integration.Tests.Controllers
{
    [SetUpFixture]
    public class BaseSetupTest
    {
        public  static HttpClient Client;
        protected static TestServer server;

        [OneTimeSetUp]
        public void RunBeforeAllTests()
        {
            // Arrange
            server = new TestServer(new WebHostBuilder()
                .ConfigureServices(s => s.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<StartupIntegrationTest>());

            Client = server.CreateClient();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            Client.Dispose();
            server.Dispose();
        }
    }
}