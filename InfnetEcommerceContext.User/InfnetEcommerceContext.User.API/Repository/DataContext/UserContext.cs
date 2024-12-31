using InfnetEcommerceContext.User.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfnetEcommerceContext.User.API.Repository.DataContext
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);

        }


    }
}
