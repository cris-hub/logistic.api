using AuthenticationAPI.test;
using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Enums;
using LogisticAPI.Mappers;
using LogisticAPI.models;
using LogisticAPI.Repositories;
using LogisticAPI.Repository;
using LogisticAPI.Services;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace LogisticAPI.Test.Services
{
    public class ProductServiceTest
    {
        Mock<IProductRepository> productRepository = new();
        Mock<IConveyanceRepository> conveyanceRepository = new();

        Mock<IPrincipal> principal = new();
        IProductService productService;
        MapperConfiguration mockMapper;
        IMapper mapper;
        public ProductServiceTest()
        {
            mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new LogisticAutoMapper()); //your automapperprofile 
           });
            mapper = mockMapper.CreateMapper();
            TestPrincipal principalMock = new TestPrincipal(new Claim("name", "John Doe"));
            principal.SetupGet(c => c.Identity).Returns(principalMock.Identity);

            productService = new ProductService(productRepository.Object, conveyanceRepository.Object, mapper, principal.Object);
        }


        [Fact]
        public async Task CreateProductAmountMoreThanTenMarineDiscountTestAsync()
        {
            double finalePrice = 97;
            ProductRequest expected = new()
            {
                DeliveryDay = new DateTime(2069, 05, 09, 9, 15, 0),
                Amount = 11,
                ProductType = "PC",
                ConveyanceId = "ABC1234D",
                Price = 100,
                PlaceId = "id",
            };
            Product entity = new() { };
            Conveyance conveyance = new() { Id = "ABC1234D",TransportType = TransportEnum.MARINE_TRANSPORT };
            conveyanceRepository.Setup(c => c.GetById(It.IsAny<string>())).ReturnsAsync(conveyance).Verifiable();
            productRepository.Setup(c => c.CreateProduct(It.IsAny<Product>())).ReturnsAsync(entity).Verifiable();

            ProductResponse actual = await productService.CreateProduct(expected);


            Assert.NotNull(actual.Id);
            Assert.True(actual.Id.Length == 10);
            Assert.Equal(finalePrice, actual.FinalPrice);
            Assert.Equal(DiscountEnum.MARINE_TRANSPORT, actual.Discount);
            Assert.Equal(expected.Amount, actual.Amount);
        }

        [Fact]
        public async Task CreateProductAmountMoreThanTenGroundDiscountTestAsync()
        {
            double finalePrice = 95;
            ProductRequest expected = new()
            {
                DeliveryDay = new DateTime(2069, 05, 09, 9, 15, 0),
                Amount = 11,
                ProductType = "PC",
                ConveyanceId = "ABC123",
                Price = 100,
                PlaceId = "id",
            };
            Product entity = new() { };
            Conveyance conveyance = new() { Id = "ABC123", TransportType = TransportEnum.GROUND_TRANSPORT };
            conveyanceRepository.Setup(c => c.GetById(It.IsAny<string>())).ReturnsAsync(conveyance).Verifiable();
            productRepository.Setup(c => c.CreateProduct(It.IsAny<Product>())).ReturnsAsync(entity);

            ProductResponse actual = await productService.CreateProduct(expected);


            Assert.NotNull(actual.Id);
            Assert.True(actual.Id.Length == 10);
            Assert.Equal(finalePrice, actual.FinalPrice);
            Assert.Equal(DiscountEnum.GROUND_TRANSPORT, actual.Discount);
            Assert.Equal(expected.Amount, actual.Amount);
        }

        [Fact]
        public async Task CreateProductWithAlfanumericIdTestAsync()
        {
            ProductRequest expected = new()
            {
                DeliveryDay = new DateTime(2069, 05, 09, 9, 15, 0),
                Amount = 11,
                ProductType = "PC",
                ConveyanceId = "",
                Price = 100,
                PlaceId = "id",
            };
            Product entity = new() { };
            Conveyance conveyance = new() { Id = "ABC123", TransportType = TransportEnum.GROUND_TRANSPORT };
            conveyanceRepository.Setup(c => c.GetById(It.IsAny<string>())).ReturnsAsync(conveyance).Verifiable();
            productRepository.Setup(c => c.CreateProduct(It.IsAny<Product>())).ReturnsAsync(entity);

            ProductResponse actual = await productService.CreateProduct(expected);


            Assert.NotNull(actual.Id);
            Assert.True(actual.Id.Length == 10);
            Assert.Equal(expected.Amount, actual.Amount);
        }

        [Fact]
        public async Task GetProdutsByUserIdAsync()
        {
            string userId = "1234";
            productRepository.Setup(c => c.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(TestDataHelper.GetFakeProductsList()).Verifiable();


            IEnumerable<ProductResponse> produdts = await productService.GetProdutsByUserId(userId);

            Assert.NotNull(produdts);
            Assert.True(produdts.ToList().Count > 0);
        }

        [Fact]
        public async Task GetProductByIdAsync()
        {
            string idProduct = "ABC1234D";
            var entity = new Product();
            productRepository.Setup(c => c.GetById(It.IsAny<string>())).ReturnsAsync(entity).Verifiable();

            var actual = await productService.GetById(idProduct);

            Assert.NotNull(actual);
        }
    }
    public class TestPrincipal : ClaimsPrincipal
    {
        public TestPrincipal(params Claim[] claims) : base(new TestIdentity(claims))
        {
        }
    }

    public class TestIdentity : ClaimsIdentity
    {
        public TestIdentity(params Claim[] claims) : base(claims)
        {
        }
    }
}
