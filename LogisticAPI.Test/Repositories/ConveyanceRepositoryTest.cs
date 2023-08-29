using LogisticAPI.Entities;
using LogisticAPI.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using LogisticAPI.DatabaseContext;
using AuthenticationAPI.test;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace LogisticAPI.Test.Repositories
{
    public class ConveyanceRepositoryTest
    {
        public required IConveyanceRepository repository;
        public Mock<IDbContextFactory> factory = new();
        public Mock<BaseContext> context = new();


        public ConveyanceRepositoryTest()
        {
            factory.Setup(c => c.GetContext(It.IsAny<string>())).Returns(context.Object);
            repository = new ConveyanceRepository(factory.Object);
        }
        [Fact]
        public async Task CreateConveyanceAsync()
        {
            Conveyance conveyance = new Conveyance();
            EntityEntry<Conveyance> entry = CreateEntityMock(conveyance);
            context.Setup(c => c.AddAsync<Conveyance>(It.IsAny<Conveyance>(), It.IsAny<CancellationToken>())).ReturnsAsync(entry).Verifiable();
            context.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            Conveyance actual = await repository.CreateConveyance(conveyance);

            Assert.NotNull(actual);
        }

        [Fact]
        public async Task GetPlacesTestAsync()
        {
            factory.Setup(c => c.GetContext(It.IsAny<string>())).Returns(context.Object);
            context.Setup<DbSet<Conveyance>>(x => x.Conveyances).ReturnsDbSet(TestDataHelper.GetFakeConveyancesList());

            repository = new ConveyanceRepository(factory.Object);


            IEnumerable<Conveyance> actual = ((List<Conveyance>)await repository.GetConveyances());


            Assert.NotNull(actual);
            Assert.True(actual.Any());
        }
        private static EntityEntry<Conveyance> CreateEntityMock(Conveyance product)
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
            var entry = new EntityEntry<Conveyance>(internalEntity);
            return entry;
        }
    }
}
