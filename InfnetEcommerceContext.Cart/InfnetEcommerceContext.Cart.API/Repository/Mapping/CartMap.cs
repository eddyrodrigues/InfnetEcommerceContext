using InfnetEcommerceContext.Cart.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetEcommerceContext.Cart.API.Repository.Mapping
{
    public class CartMap : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.HasMany<ProductEntity>().WithOne();
            builder.HasKey(c => c.Id);
        }
    }
}
