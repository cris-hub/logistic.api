using LogisticAPI.Controllers;
using LogisticAPI.Entities;
using LogisticAPI.models;
using LogisticAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace LogisticAPI.Test.Controllers
{
    public class PlaceControllerTest
    {
        Mock<IPlaceService> service = new();

        [Fact]
        public async Task CreateProductAsync()
        {
            PlaceRequest request = new();
            PlaceResponse response = new();
            PlaceController controller = new PlaceController(service.Object);
            service.Setup(c => c.CreatePlace(It.IsAny<PlaceRequest>())).ReturnsAsync(response);

            IActionResult result = await controller.CreatePlace(request);

            Assert.NotNull(((ObjectResult)result).Value);
            Assert.Equal((int)HttpStatusCode.OK, ((ObjectResult)result).StatusCode);
        }

    }
}