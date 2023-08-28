using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Mappers;
using LogisticAPI.models;
using LogisticAPI.Repositories;
using LogisticAPI.Services;
using Moq;

namespace LogisticAPI.Test.Services
{
    public class PlaceServiceTest
    {
        public IPlaceService service;
        public Mock<IPlaceRepository> mock = new();
        MapperConfiguration mockMapper;
        IMapper mapper;
        public PlaceServiceTest()
        {
            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LogisticAutoMapper()); //your automapperprofile 
            });
            mapper = mockMapper.CreateMapper();
            service = new PlaceService(mock.Object, mapper);
        }

        [Fact]
        public async Task CreateTestAsync()
        {
            PlaceRequest request = new PlaceRequest();
            Place entity = new Place();
            mock.Setup(c => c.CreatePlace(It.IsAny<Place>())).ReturnsAsync(entity);

            PlaceResponse actual = await service.CreatePlace(request);

            Assert.NotNull(actual);
        }
    }
}
