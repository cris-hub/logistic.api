using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Enums;
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
            CreateMap<Conveyance, ConveyanceResponse>().ForPath(c => c.TransportTypeValue, opt => opt.MapFrom(src => this.GetValueFromTransportType(src.TransportType))); ;
            CreateMap<ConveyanceRequest, ConveyanceResponse>();
        }

        private string GetValueFromTransportType(TransportEnum transportType)
        {
            switch (transportType)
            {

                case TransportEnum.GROUND_TRANSPORT:
                    return "Ground Transport";
                case TransportEnum.MARINE_TRANSPORT:
                    return "Marine Transport";
                default:
                    return "";
            }
        }
    }
}