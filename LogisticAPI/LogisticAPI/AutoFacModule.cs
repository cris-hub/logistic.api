using Autofac;
using LogisticAPI.DatabaseContext;
using LogisticAPI.Repositories;
using LogisticAPI.Repository;
using LogisticAPI.Services;

namespace LogisticAPI
{
    public class AutoFacModule : Module
    {
        private readonly IConfiguration _configuration;
        public AutoFacModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration).As<IConfiguration>();
            builder.Register<IDbContextFactory>(ctx =>
            {
                var allContext = new Dictionary<string, BaseContext>
                {
                    { "LogisticContext", ctx.Resolve<LogisticContext>() }
                };
                return new DbContextFactory(allContext);

            });

            builder.RegisterType<PlaceRepository>().As<IPlaceRepository>();
            builder.RegisterType<PlaceService>().As<IPlaceService>();

            builder.RegisterType<ConveyanceRepository>().As<IConveyanceRepository>();
            builder.RegisterType<ConveyanceService>().As<IConveyanceService>();

            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductService>().As<IProductService>();

        }
    }
}
