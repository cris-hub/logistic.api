using LogisticAPI.DatabaseContext;
using LogisticAPI.Entities;
using LogisticAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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
            var result = await db.Products.AddAsync(entity);

            await db.SaveChangesAsync();

            return await db.Products.Include(c => c.Place).FirstOrDefaultAsync(c => c.Id == result.Entity.Id);
        }

        public Task<Product> GetById(string id)
        {
            BaseContext db = @object.GetContext(context);
            return db.Products.Include(c => c.Conveyance).Include(c => c.Place).FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Product>> GetByUserIdAsync(string id)

        {
            BaseContext db = @object.GetContext(context);
            return db.Products.Include(c => c.Conveyance).Include(c => c.Place).Where(c => c.UserId == id).ToListAsync();
        }
    }
}