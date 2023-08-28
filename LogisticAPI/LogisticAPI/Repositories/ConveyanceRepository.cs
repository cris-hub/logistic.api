using LogisticAPI.DatabaseContext;
using LogisticAPI.Entities;

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
    }
}