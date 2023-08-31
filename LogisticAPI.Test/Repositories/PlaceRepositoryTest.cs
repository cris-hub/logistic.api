using AuthenticationAPI.test;
using LogisticAPI.DatabaseContext;
using LogisticAPI.Entities;
using LogisticAPI.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Moq.EntityFrameworkCore;

namespace LogisticAPI.Test.Repositories
{
    public class PlaceRepositoryTest
    {
        public required IPlaceRepository repository;
        public Mock<IDbContextFactory> factory = new();
        public Mock<BaseContext> context = new();


        public PlaceRepositoryTest()
        {
            factory.Setup(c => c.GetContext(It.IsAny<string>())).Returns(context.Object);
            repository = new PlaceRepository(factory.Object);
        }
        [Fact]
        public async Task CreatePlaceTestAsync()
        {
            Place place = new Place();
            EntityEntry<Place> entry = CreateEntityMock(place);
            context.Setup(c => c.AddAsync<Place>(It.IsAny<Place>(), It.IsAny<CancellationToken>())).ReturnsAsync(entry).Verifiable();
            context.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            Place actual = await repository.CreatePlace(place);

            Assert.NotNull(actual);
        }

        [Fact]
        public async Task GetPlacesTestAsync()
        {
            factory.Setup(c => c.GetContext(It.IsAny<string>())).Returns(context.Object);
            context.Setup<DbSet<Place>>(x => x.Places).ReturnsDbSet(TestDataHelper.GetFakePlacesList());

            repository = new PlaceRepository(factory.Object);


            IEnumerable<Place> actual = (await repository.GetPlaces());


            Assert.NotNull(actual);
            Assert.True(actual.Any());
        }


        private static EntityEntry<Place> CreateEntityMock(Place product)
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
            EntityEntry<Place> entry = new(internalEntity);
            return entry;
        }
    }
}
