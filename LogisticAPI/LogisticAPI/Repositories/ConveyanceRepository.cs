using LogisticAPI.DatabaseContext;
using LogisticAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticAPI.Repositories
{
    public class ConveyanceRepository : IConveyanceRepository
    {
        private IDbContextFactory @object;
        private string context = "LogisticContext";

        public ConveyanceRepository(IDbContextFactory @object)
        {
            this.@object = @object;
        }

        public async Task<Conveyance> CreateConveyance(Conveyance conveyance)
        {
            BaseContext db = @object.GetContext(context);
            var result = await db.AddAsync(conveyance);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public Task<Conveyance> GetById(string conveyanceId)
        {
            BaseContext db = @object.GetContext(context);
            return db.Conveyances.FirstOrDefaultAsync(c => c.Id == conveyanceId);
        }

        public Task<List<Conveyance>> GetConveyances()
        {
            BaseContext db = @object.GetContext(context);
            return db.Conveyances.ToListAsync();
        }
    }
}