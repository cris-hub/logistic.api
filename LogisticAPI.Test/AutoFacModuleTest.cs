using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using LogisticAPI.DatabaseContext;
using LogisticAPI.Mappers;
using LogisticAPI.Repositories;
using LogisticAPI.Repository;
using LogisticAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Security.Principal;
using System.Text;

namespace LogisticAPI.Test
{
    public class AutoFacModuleTest
    {
        private readonly AutoFacModule module;
        private readonly Mock<IConfiguration> config = new();
        private readonly Mock<IConfigurationSection> contection = new();
        private readonly Mock<IPrincipal> principal = new();
        private readonly Mock<IHttpContextAccessor> gg = new();

        MapperConfiguration mockMapper;
        IMapper mapper;

        protected IContainer container { get; private set; }

        public AutoFacModuleTest()
        {
            contection.Setup(c => c.Value).Returns("Data Source=localhost; Initial Catalog=logistitic; User Id=admin; Password=admin;Trusted_Connection=True;TrustServerCertificate=True;");
            config.Setup(x => x.GetSection(It.Is<string>(k => k == "ConnectionStrings:LogisticContext"))).Returns(contection.Object);
            module = new(config.Object);
            var builder = new ContainerBuilder();

            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LogisticAutoMapper()); //your automapperprofile 
            });
            mapper = mockMapper.CreateMapper();
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(typeof(Program));
            serviceCollection.AddSingleton(mapper);
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddSingleton<IPrincipal>(principal.Object);
            serviceCollection.AddDbContext<LogisticContext>(options => options.UseInMemoryDatabase("LogisticContext"));


            serviceCollection.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     IssuerSigningKey = new SymmetricSecurityKey
                         (Encoding.ASCII.GetBytes("")),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = false,
                     ValidateIssuerSigningKey = true
                 };
             });
            builder.Populate(serviceCollection);
            builder.RegisterModule(module);

            container = builder.Build();

        }

        [Fact]
        public void ConfigurationTest()
        {
            IConfiguration config = container.Resolve<IConfiguration>();

            Assert.NotNull(config);
        }
        [Fact]
        public void IDbContextFactoryTest()
        {
            IPlaceRepository config = container.Resolve<IPlaceRepository>();

            Assert.NotNull(config);
        }


        [Fact]
        public void IPlaceRepositoryTest()
        {
            IPlaceRepository config = container.Resolve<IPlaceRepository>();

            Assert.NotNull(config);
        }


        [Fact]
        public void IPlaceServiceTest()
        {
            IPlaceService config = container.Resolve<IPlaceService>();

            Assert.NotNull(config);
        }
        [Fact]
        public void IConveyanceRepositoryTest()
        {
            IConveyanceRepository config = container.Resolve<IConveyanceRepository>();

            Assert.NotNull(config);
        }
        [Fact]
        public void IConveyanceServiceTest()
        {
            IConveyanceService config = container.Resolve<IConveyanceService>();

            Assert.NotNull(config);
        }
        [Fact]
        public void IProductRepositoryTest()
        {
            IProductRepository config = container.Resolve<IProductRepository>();

            Assert.NotNull(config);
        }
        [Fact]
        public void IProductServiceTest()
        {
            IProductService config = container.Resolve<IProductService>();

            Assert.NotNull(config);
        }
    }
}
