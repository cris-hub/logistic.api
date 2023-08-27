using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.models;

namespace LogisticAPI
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();
            CreateMap<ProductRequest, ProductResponse>();
        }
    }
}
