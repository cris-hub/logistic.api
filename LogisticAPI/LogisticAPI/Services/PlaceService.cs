using AutoMapper;
using Azure.Core;
using LogisticAPI.Entities;
using LogisticAPI.models;
using LogisticAPI.Models;
using LogisticAPI.Repositories;

namespace LogisticAPI.Services
{
    public class PlaceService : IPlaceService
    {
        private IPlaceRepository @object;
        private readonly IMapper mapper;

        public PlaceService(IPlaceRepository @object, IMapper mapper)
        {
            this.@object = @object;
            this.mapper = mapper;
        }

        public async Task<PlaceResponse> CreatePlace(PlaceRequest request)
        {
            var entity = mapper.Map<Place>(request);
            entity = await @object.CreatePlace(entity);
            var result = mapper.Map<PlaceResponse>(entity);

            return result;
        }
    }
}