using LogisticAPI.Entities;

namespace LogisticAPI.Repositories
{
    public interface IPlaceRepository
    {
        Task<Place> CreatePlace(Place place);
    }
}