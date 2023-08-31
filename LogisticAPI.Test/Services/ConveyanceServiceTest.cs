using AuthenticationAPI.test;
using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Enums;
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

        [Fact]
        public async Task CreateIdForGroundTransportAsync()
        {
            string validId = "ABC123";
            ConveyanceRequest request = new ConveyanceRequest() { TransportType = TransportEnum.GROUND_TRANSPORT, Id = validId };
            Conveyance entity = new Conveyance();
            mock.Setup(c => c.CreateConveyance(It.IsAny<Conveyance>())).ReturnsAsync(entity);


            ConveyanceResponse actual = await service.CreateConveyance(request);

            Assert.NotNull(actual);
            Assert.Null(actual.Errors);
        }

        [Fact]
        public async Task CreateInvalidIdForGroundTransportAsync()
        {
            string invalidId = "anywrongvalue";
            ConveyanceRequest request = new ConveyanceRequest() { TransportType = TransportEnum.GROUND_TRANSPORT, Id = invalidId };
            Conveyance entity = new Conveyance();
            mock.Setup(c => c.CreateConveyance(It.IsAny<Conveyance>())).ReturnsAsync(entity);


            ConveyanceResponse actual = await service.CreateConveyance(request);

            Assert.NotNull(actual);
            Assert.False(actual.Errors.Count == 0);
            Assert.True(actual.Errors.Any(c => c.IdString == ErrorTypeEnum.IdFormatTransportGroundIsRequired.IdString));
        }

        [Fact]
        public async Task CreateIdForMaritimeTransportAsync()
        {
            string validId = "ABC1234D";
            ConveyanceRequest request = new ConveyanceRequest() { TransportType = TransportEnum.MARINE_TRANSPORT, Id = validId };
            Conveyance entity = new Conveyance();
            mock.Setup(c => c.CreateConveyance(It.IsAny<Conveyance>())).ReturnsAsync(entity);


            ConveyanceResponse actual = await service.CreateConveyance(request);

            Assert.NotNull(actual);
            Assert.Null(actual.Errors);
        }
        [Fact]
        public async Task CreateInvalidIdForMaritimeTransportAsync()
        {
            string invalidId = "anywrongvalue";
            ConveyanceRequest request = new ConveyanceRequest() { TransportType = TransportEnum.MARINE_TRANSPORT, Id = invalidId };
            Conveyance entity = new Conveyance();
            mock.Setup(c => c.CreateConveyance(It.IsAny<Conveyance>())).ReturnsAsync(entity);


            ConveyanceResponse actual = await service.CreateConveyance(request);

            Assert.NotNull(actual);
            Assert.False(actual.Errors.Count == 0);
            Assert.True(actual.Errors.Any(c => c.IdString == ErrorTypeEnum.IdFormatTransportMaritimeIsRequired.IdString));
        }

        [Fact]
        public async Task CreateInvalidTypeTransportAsync()
        {
            string invalidId = "anywrongvalue";
            ConveyanceRequest request = new ConveyanceRequest() { TransportType = TransportEnum.NONE, Id = invalidId };
            Conveyance entity = new Conveyance();
            mock.Setup(c => c.CreateConveyance(It.IsAny<Conveyance>())).ReturnsAsync(entity);


            ConveyanceResponse actual = await service.CreateConveyance(request);

            Assert.NotNull(actual);
            Assert.False(actual.Errors.Count == 0);
            Assert.True(actual.Errors.Any(c => c.IdString == ErrorTypeEnum.TypeTransportIsRequired.IdString));
        }


        [Fact]
        public async Task ListTestAsync()
        {
            mock.Setup(c => c.GetConveyances()).ReturnsAsync(TestDataHelper.GetFakeConveyancesList());



            IEnumerable<ConveyanceResponse> actual = ((List<ConveyanceResponse>)await service.GetConveyances());


            Assert.NotNull(actual);
            Assert.True(actual.Any());

        }
    }
}
