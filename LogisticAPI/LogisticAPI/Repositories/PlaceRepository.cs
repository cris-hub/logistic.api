using LogisticAPI.DatabaseContext;
using LogisticAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticAPI.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private string context = "LogisticContext";

        private IDbContextFactory @object;

        public PlaceRepository(IDbContextFactory @object)
        {
            this.@object = @object;
        }

        public async Task<Place> CreatePlace(Place place)
        {
            BaseContext db = @object.GetContext(context);
            var result = await db.AddAsync(place);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public Task<List<Place>> GetPlaces()
        {
            BaseContext db = @object.GetContext(context);
            return db.Places.ToListAsync();
        }
    }
}