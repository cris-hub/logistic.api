using AuthenticationAPI.test;
using LogisticAPI.DatabaseContext;
using LogisticAPI.Entities;
using LogisticAPI.Enums;
using LogisticAPI.Repositories;
using LogisticAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Moq.EntityFrameworkCore;

namespace LogisticAPI.Test.Repositories
{
    public class ProductRepositoyTest
    {
        public string contextName = "LogisticContext";
        public required IProductRepository repository;
        public Mock<IDbContextFactory> factory = new();
        public Mock<BaseContext> context = new();

        [Fact]
        public async Task GetByIdAsync()
        {
            string productId = "ABC1234D";
            factory.Setup(c => c.GetContext(It.IsAny<string>())).Returns(context.Object);
            context.Setup<DbSet<Product>>(x => x.Products).ReturnsDbSet(TestDataHelper.GetFakeProductsList());
            repository = new ProductRepository(factory.Object);

            Product actual = await repository.GetById(productId);

            Assert.NotNull(actual);
            Assert.True(actual.Id == productId);
        }

        [Fact]
        public async Task GetByUserIdAsync()
        {
            string userId = "1234";
            factory.Setup(c => c.GetContext(It.IsAny<string>())).Returns(context.Object);
            context.Setup<DbSet<Product>>(x => x.Products).ReturnsDbSet(TestDataHelper.GetFakeProductsList());
            repository = new ProductRepository(factory.Object);

            List<Product> actual = await repository.GetByUserIdAsync(userId);

            Assert.NotNull(actual);
            Assert.True(actual.Count > 0);
            Assert.True(actual.All(c => c.UserId == userId));
        }


        [Fact]
        public async Task CreateProuctAsync()
        {
            Product product = new Product()
            {
                DeliveryDay = new DateTime(2069, 05, 09, 9, 15, 0),
                Amount = 1,
                ProductType = "PC",
                Price = 10.2,
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
                Discount = DiscountEnum.NONE,
                FinalPrice = 0.3

            };
            EntityEntry<Product> entry = CreateEntityMock(product);
            factory.Setup(c => c.GetContext(It.IsAny<string>())).Returns(context.Object);
            _ = context.Setup(c => c.AddAsync<Product>(It.IsAny<Product>(), It.IsAny<CancellationToken>())).ReturnsAsync(entry);
            context.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
            repository = new ProductRepository(factory.Object);

            var actual = await repository.CreateProduct(product);


            DateTime created = actual.Created;
            DateTime deliveryDay = actual.DeliveryDay;
            Assert.NotNull(actual);
            Assert.True(deliveryDay > created);

        }

        private static EntityEntry<Product> CreateEntityMock(Product product)
        {
            var stateManagerMock = new Mock<IStateManager>();
            var entityTypeMock = new Mock<IRuntimeEntityType>();
            _ = entityTypeMock
                .SetupGet(_ => _.EmptyShadowValuesFactory)
                .Returns(() => new Mock<ISnapshot>().Object);
            _ = entityTypeMock
                .SetupGet(_ => _.Counts)
                .Returns(new PropertyCounts(1, 1, 1, 1, 1, 1));
            entityTypeMock
                .Setup(_ => _.GetProperties())
                .Returns(Enumerable.Empty<IProperty>());
            var internalEntity = new InternalEntityEntry(stateManagerMock.Object,
                entityTypeMock.Object, product);
            var entry = new EntityEntry<Product>(internalEntity);
            return entry;
        }
    }
}
