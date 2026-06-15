using Microsoft.EntityFrameworkCore;
using ISO_ERP.Models;

namespace ISO_ERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ISO_ERP.Models.Category> Categories { get; set; }
        public DbSet<ISO_ERP.Models.Product> Products { get; set; }
        public DbSet<ISO_ERP.Models.ProductDetail> ProductDetails { get; set; } 
        public DbSet<ISO_ERP.Models.Detail> Details { get; set; }
        public DbSet<ISO_ERP.Models.Production> Productions { get; set; }
        public DbSet<ISO_ERP.Models.ProductionItem> ProductionItems { get; set; }
        public DbSet<ISO_ERP.Models.Inspector> Inspectors { get; set; }
        public DbSet<ISO_ERP.Models.ProductDetailSubItem> ProductDetailSubItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<ProductDetail>()
                    .HasMany(x => x.SubItems)
                    .WithOne(x => x.ProductDetail)
                    .HasForeignKey(x => x.ProductDetailId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
    }
}