using LogisticAPI.DatabaseContext;
using LogisticAPI.Entities;
using LogisticAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace LogisticAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private string context = "LogisticContext";
        private IDbContextFactory @object;

        public ProductRepository(IDbContextFactory @object)
        {
            this.@object = @object;
        }

        public async Task<Product> CreateProduct(Product entity)
        {
            BaseContext db = @object.GetContext(context);
            var result = await db.AddAsync(entity);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public Task<Product> GetById(string id)
        {
            BaseContext db = @object.GetContext(context);
            return db.Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Product>> GetByUserIdAsync(string id)

        {
            BaseContext db = @object.GetContext(context);
            return db.Products.Where(c => c.UserId == id).ToListAsync();
        }
    }
}