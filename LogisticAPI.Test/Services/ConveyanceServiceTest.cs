using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Mappers;
using LogisticAPI.Models;
using LogisticAPI.Repositories;
using LogisticAPI.Services;
using Moq;

namespace LogisticAPI.Test.Services
{
    public class ConveyanceServiceTest
    {
        public IConveyanceService service;
        public Mock<IConveyanceRepository> mock = new();
        MapperConfiguration mockMapper;
        IMapper mapper;

        public ConveyanceServiceTest()
        {
            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LogisticAutoMapper()); //your automapperprofile 
            });
            mapper = mockMapper.CreateMapper();
            service = new ConveyanceService(mock.Object, mapper);
        }

        [Fact]
        public async Task CreateTestAsync()
        {
            ConveyanceRequest request = new ConveyanceRequest();
            Conveyance entity = new Conveyance();
            mock.Setup(c => c.CreateConveyance(It.IsAny<Conveyance>())).ReturnsAsync(entity);


            ConveyanceResponse actual = await service.CreateConveyance(request);

            Assert.NotNull(actual);
        }
    }
}
