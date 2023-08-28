using LogisticAPI.Entities;

namespace LogisticAPI.Repositories
{
    public interface IConveyanceRepository
    {
        Task<Conveyance> CreateConveyance(Conveyance conveyance);
    }
}