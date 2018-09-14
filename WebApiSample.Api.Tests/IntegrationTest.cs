using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebApiSample.DataAccess.Models;
using Xunit;

namespace WebApiSample.Api.Tests
{
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<WebApiSample.Api.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<WebApiSample.Api.Startup> _factory;

        public IntegrationTest(
            CustomWebApplicationFactory<WebApiSample.Api.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        private static StringContent ConvertObjectToStringContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }


        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var p = new Product
            {
                Description = "s"
            };
            var result = await _client.PostAsync("/api/products", ConvertObjectToStringContent(p));

            Console.WriteLine("StatusCode = " + result.StatusCode);
            // Assert
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Create_ReturnsOK()
        {
            // Arrange & Act
            var p = new Product
            {
                Name = "titi",
                Description = "s"
            };
            var result = await _client.PostAsync("/api/products", ConvertObjectToStringContent(p));

            Console.WriteLine("StatusCode = " + result.StatusCode);
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}