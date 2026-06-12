using Microsoft.EntityFrameworkCore;
using Erp.Api.Model;

namespace Erp.Api.Data
{
    public class ErpDbContext : DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options) 
        { }

        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("manufacturing");
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.OrderNumber)
                    .HasMaxLength(50);

                entity.Property(x => x.ProductCode)
                    .HasMaxLength(50);

                entity.Property(x => x.Status)
                    .HasMaxLength(20);
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
