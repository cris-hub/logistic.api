using LogisticAPI.Models;

namespace LogisticAPI.Services
{
    public interface IConveyanceService
    {
        Task<ConveyanceResponse> CreateConveyance(ConveyanceRequest entity);
    }
}