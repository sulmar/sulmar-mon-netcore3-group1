using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sulmar.Shopping.IntegrationTests
{
    // dotnet add package Microsoft.AspNetCore.TestHost
    public class Tests
    {
        private TestServer server;
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            var builder = new WebHostBuilder()
               .UseStartup<API.Startup>()
               .UseEnvironment("Development")
               .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build());

            server = new TestServer(builder);

            client = server.CreateClient();
        }


        [Test]
        public async Task Get_ExistsCustomer_ReturnsCustomer()
        {
            // Act
            var response = await client.GetAsync("api/customers/1");

            // var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}