using AuthenticationAPI.test;
using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Enums;
using LogisticAPI.models;
using LogisticAPI.Repository;
using LogisticAPI.Services;
using LogisticAPI.Test.Repositories;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace LogisticAPI.Test.Services
{
    public class ProductServiceTest
    {
        Mock<IProductRepository> repository = new();
        Mock<IPrincipal> principal = new();
        IProductService productService;
        MapperConfiguration mockMapper;
        IMapper mapper;
        public ProductServiceTest()
        {
            mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new AutoMapper()); //your automapperprofile 
           });
            mapper = mockMapper.CreateMapper();
            TestPrincipal c = new TestPrincipal(new Claim("name", "John Doe"));
            principal.SetupGet(c => c.Identity).Returns(c.Identity);

            productService = new ProductService(repository.Object, mapper, principal.Object);
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
                Conveyance = new Conveyance()
                {
                    Id = "CFV-231",
                    TransportType = TransportEnum.MARINE_TRANSPORT
                },
                Price = 100,
                Place = new Place()
                {
                    Id = "",
                    PlaceType = PlaceEnum.PORT,
                    City = "",
                    Country = ""
                },
            };
            Product entity = new() { };

            repository.Setup(c => c.CreateProduct(It.IsAny<Product>())).ReturnsAsync(entity);

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
                Conveyance = new Conveyance()
                {
                    Id = "CFV-231",
                    TransportType = TransportEnum.GROUND_TRANSPORT
                },
                Price = 100,
                Place = new Place()
                {
                    Id = "",
                    PlaceType = PlaceEnum.PORT,
                    City = "",
                    Country = ""
                },
            };
            Product entity = new() { };

            repository.Setup(c => c.CreateProduct(It.IsAny<Product>())).ReturnsAsync(entity);

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
                Amount = 1,
                ProductType = "PC",
                Conveyance = new Conveyance()
                {
                    Id = "CFV-231",
                    TransportType = TransportEnum.GROUND_TRANSPORT
                },
                Place = new Place()
                {
                    Id = "",
                    PlaceType = PlaceEnum.PORT,
                    City = "",
                    Country = ""
                },
            };
            Product entity = new() { };

            repository.Setup(c => c.CreateProduct(It.IsAny<Product>())).ReturnsAsync(entity);

            ProductResponse actual = await productService.CreateProduct(expected);


            Assert.NotNull(actual.Id);
            Assert.True(actual.Id.Length == 10);
            Assert.Equal(expected.Amount, actual.Amount);
        }

        [Fact]
        public async Task GetProdutsByUserIdAsync()
        {
            string userId = "1234";
            repository.Setup(c => c.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(TestDataHelper.GetFakeProductsList()).Verifiable();


            IEnumerable<ProductResponse> produdts = await productService.GetProdutsByUserId(userId);

            Assert.NotNull(produdts);
            Assert.True(produdts.ToList().Count > 0);
        }

        [Fact]
        public async Task GetProductByIdAsync()
        {
            string idProduct = "ABC1234D";
            var entity = new Product();
            repository.Setup(c => c.GetById(It.IsAny<string>())).ReturnsAsync(entity).Verifiable();

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
