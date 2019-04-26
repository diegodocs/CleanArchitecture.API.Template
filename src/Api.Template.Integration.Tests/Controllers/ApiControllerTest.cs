using System.Net.Http;
using NUnit.Framework;

namespace Api.Template.Integration.Tests.Controllers
{
    [TestFixture]
    public class ApiControllerTest
    {
        protected HttpClient client;        

        public ApiControllerTest()
        {
            client = BaseSetupTest.Client;
            client.DefaultRequestHeaders.Add("UserId", "9ae42a23-1e01-4402-bc3f-c74a8ee295c6");
        }
    }
}