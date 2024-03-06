using InfnetEcommerceContext.Order.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfnetEcommerceContext.Order.API.Repository.DataContext
{
    public class OrderContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
        }

    }
}
