using LogisticAPI.Entities;
using LogisticAPI.models;

namespace LogisticAPI.Services
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<ProductResponse> GetById(string id);
        Task<IEnumerable<ProductResponse>> GetProdutsByUserId(string userId);
    }
}