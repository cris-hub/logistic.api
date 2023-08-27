using AuthenticationAPI.test;
using LogisticAPI.Controllers;
using LogisticAPI.models;
using LogisticAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace LogisticAPI.Test.Controllers
{
    public class ProductControllerTest
    {
        Mock<IProductService> productServiceMock = new();

        [Fact]
        public void AuthorizationValitorGetByUserIdTest()
        {
            string id = "ABC1234D";
            ProductResponse product = new ProductResponse() { Id = "ABC1234D" };
            productServiceMock.Setup(c => c.GetProdutsByUserId(id)).ReturnsAsync(TestDataHelper.GetFakeProductsResponseList());
            ProductController controller = new ProductController(productServiceMock.Object);
            var actualAttribute = controller.GetType().GetMethod("GetByUserId").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            var result = controller.GetByUserId(id);

            var p = result.Result as ObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, p.StatusCode);
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());

        }
        [Fact]
        public void AuthorizationValitorCreateProductAsyncTest()
        {
            string id = "ABC1234D";
            ProductResponse product = new ProductResponse() { Id = "ABC1234D" };
            productServiceMock.Setup(c => c.CreateProduct(It.IsAny<ProductRequest>())).ReturnsAsync(product);
            ProductController controller = new ProductController(productServiceMock.Object);
            var actualAttribute = controller.GetType().GetMethod("CreateProduct").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            var result = controller.CreateProduct(new() { });

            var p = result.Result as ObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, p.StatusCode);
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());

        }

        [Fact]
        public async Task GetByIdTestAsync()
        {
            string id = "ABC1234D";
            ProductController controller = new ProductController(productServiceMock.Object);
            productServiceMock.Setup(c => c.GetById(It.IsAny<string>())).ReturnsAsync(TestDataHelper.GetFakeProductsResponseList().First());

            IActionResult result = await controller.GetById(id);

            Assert.NotNull(((ObjectResult)result).Value);
            Assert.Equal((int)HttpStatusCode.OK, ((ObjectResult)result).StatusCode);

        }

        [Fact]
        public async Task CreateProductAsync()
        {
            ProductRequest request = new();
            ProductResponse respomse = new();
            ProductController controller = new ProductController(productServiceMock.Object);
            productServiceMock.Setup(c => c.CreateProduct(It.IsAny<ProductRequest>())).ReturnsAsync(respomse);

            IActionResult result = await controller.CreateProduct(request);

            Assert.NotNull(((ObjectResult)result).Value);
            Assert.Equal((int)HttpStatusCode.OK, ((ObjectResult)result).StatusCode);
        }


        [Fact]
        public async Task GetByUserIdAsync()
        {

            string id = "ABC1234D";
            ProductController controller = new(productServiceMock.Object);
            productServiceMock.Setup(c => c.GetProdutsByUserId(It.IsAny<string>())).ReturnsAsync(TestDataHelper.GetFakeProductsResponseList());

            IActionResult result = await controller.GetByUserId(id);

            List<ProductResponse> actual = ((ObjectResult)result).Value as List<ProductResponse>;
            Assert.NotNull(((ObjectResult)result).Value);
            Assert.True(actual.Count > 0);
            Assert.Equal((int)HttpStatusCode.OK, ((ObjectResult)result).StatusCode);
        }

    }
}
