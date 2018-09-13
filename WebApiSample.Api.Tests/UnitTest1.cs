using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiSample.Api.Controllers;
using WebApiSample.DataAccess.Repositories;
using Xunit;

namespace WebApiSample.Api.Tests
{
    public class UnitTest1
    {

        
        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IProductsRepository>();
            var controller = new ProductsController(mockRepo.Object);
            controller.ModelState.AddModelError("error","some error");

            // Act
            var result = await controller.CreateAsync(product: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
