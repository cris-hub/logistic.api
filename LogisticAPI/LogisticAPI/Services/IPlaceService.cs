using LogisticAPI.Entities;
using LogisticAPI.models;

namespace LogisticAPI.Services
{
    public interface IPlaceService
    {
        Task<PlaceResponse> CreatePlace(PlaceRequest place);
        Task<List<PlaceResponse>> GetPlaces();
    }
}