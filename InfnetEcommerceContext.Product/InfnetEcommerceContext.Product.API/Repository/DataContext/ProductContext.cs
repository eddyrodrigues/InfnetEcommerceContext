using InfnetEcommerceContext.Product.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InfnetEcommerceContext.Product.API.Repository.DataContext
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductContext).Assembly);

            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity("Fluence", "Fluence 2013", 50000, new System.Guid("a639abb5-4a2b-44c6-b3eb-672c00c2b88f")));
            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity("Megani", "Megani 2010", 40000, new System.Guid("b639abb5-4a2b-44c6-b3eb-672c00c2b88f")));
            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity("Sandero", "Sandero 2010", 44400, new System.Guid("c639abb5-4a2b-44c6-b3eb-672c00c2b88f")));
            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity("Gol", "Gol 2011", 52000, new System.Guid("d639abb5-4a2b-44c6-b3eb-672c00c2b88f")));
            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity("Onix", "Onix 2015", 63000, new System.Guid("e639abb5-4a2b-44c6-b3eb-672c00c2b88f")));

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
