using LogisticAPI.Controllers;
using LogisticAPI.Entities;
using LogisticAPI.Models;
using LogisticAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace LogisticAPI.Test.Controllers
{
    public class ConveyanceControllerTest
    {
        Mock<IConveyanceService> service = new();

        [Fact]
        public async Task CreateProductAsync()
        {
            ConveyanceRequest request = new();
            ConveyanceResponse response = new();
            ConveyanceController controller = new ConveyanceController(service.Object);
            service.Setup(c => c.CreateConveyance(It.IsAny<ConveyanceRequest>())).ReturnsAsync(response);

            IActionResult result = await controller.CreatePlace(request);

            Assert.NotNull(((ObjectResult)result).Value);
            Assert.Equal((int)HttpStatusCode.OK, ((ObjectResult)result).StatusCode);
        }
    }
}
