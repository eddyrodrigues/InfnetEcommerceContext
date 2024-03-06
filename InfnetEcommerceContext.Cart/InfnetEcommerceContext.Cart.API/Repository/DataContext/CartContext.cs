using InfnetEcommerceContext.Cart.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfnetEcommerceContext.Cart.API.Repository.DataContext
{
    public class CartContext : DbContext
    {
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartContext).Assembly);
        }


    }
}
