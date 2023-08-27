using LogisticAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticAPI.DatabaseContext
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options)
      : base(options)
        {
        }

        public BaseContext()
        {

        }
        public virtual DbSet<Product> Products { get; set; } = default!;
    }
}