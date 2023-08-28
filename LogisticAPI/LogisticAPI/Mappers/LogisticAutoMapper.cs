using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.models;
using LogisticAPI.Models;

namespace LogisticAPI.Mappers
{
    public class LogisticAutoMapper : Profile
    {
        public LogisticAutoMapper()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, ProductResponse>();

            CreateMap<PlaceRequest, Place>();
            CreateMap<Place, PlaceResponse>();
            CreateMap<PlaceRequest, PlaceResponse>();

            CreateMap<ConveyanceRequest, Conveyance>();
            CreateMap<Conveyance, ConveyanceResponse>();
            CreateMap<ConveyanceRequest, ConveyanceResponse>();
        }
    }
}
