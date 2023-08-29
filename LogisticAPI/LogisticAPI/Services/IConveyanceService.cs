using LogisticAPI.Entities;
using LogisticAPI.Models;

namespace LogisticAPI.Services
{
    public interface IConveyanceService
    {
        Task<ConveyanceResponse> CreateConveyance(ConveyanceRequest entity);
        Task<List<ConveyanceResponse>> GetConveyances();
    }
}