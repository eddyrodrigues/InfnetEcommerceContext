using InfnetEcommerceContext.Product.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetEcommerceContext.Product.API.Repository.DataContext.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products");

            builder.Property(c => c.Price).HasColumnType("float");
        }
    }
}
