using LogisticAPI.Entities;

namespace LogisticAPI.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product entity);
        Task<Product> GetById(string id);
        Task<List<Product>> GetByUserIdAsync(string id);
    }
}