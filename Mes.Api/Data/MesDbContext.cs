using Microsoft.EntityFrameworkCore;
using Mes.Api.Model;

namespace Mes.Api.Data
{
    public class MesDbContext : DbContext
    {
        public MesDbContext(DbContextOptions<MesDbContext> options) : base(options)
        { }

        public DbSet<Job> Jobs => Set<Job>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("manufacturing");
            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.JobNumber)
                    .HasMaxLength(50);

                entity.Property(x => x.ProductCode)
                    .HasMaxLength(50);
            });
            base.OnModelCreating(modelBuilder);
        }   
    }
}
