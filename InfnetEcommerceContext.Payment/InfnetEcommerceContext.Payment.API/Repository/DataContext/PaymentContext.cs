using InfnetEcommerceContext.Payment.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InfnetEcommerceContext.Payment.API.Repository.DataContext
{
    public class PaymentContext : DbContext
    {

        public DbSet<PaymentEntity> Payments { get; set; }


        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);


            modelBuilder.Entity<PaymentEntity>().HasData(
                new PaymentEntity() { 
                    Id = Guid.NewGuid(),
                    PaidAt = DateTime.Now.AddDays(-20),
                    Status = PaymentStatus.Paid,
                    UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    OrderId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
                }
            );
        }
    }
}
