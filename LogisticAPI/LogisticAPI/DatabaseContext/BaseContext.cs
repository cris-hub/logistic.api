using LogisticAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticAPI.DatabaseContext
{
    public partial class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options): base(options)
        {
        }

        public BaseContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public virtual DbSet<Product> Products { get; set; } = default!;
        public virtual DbSet<Place> Places { get; set; } = default!;
        public virtual DbSet<Conveyance> Conveyances { get; set; } = default!;
    }
}