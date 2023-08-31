using LogisticAPI.Entities;

namespace LogisticAPI.Repositories
{
    public interface IConveyanceRepository
    {
        Task<Conveyance> CreateConveyance(Conveyance conveyance);
        Task<Conveyance> GetById(string conveyanceId);
        Task<List<Conveyance>> GetConveyances();
    }
}