using InfnetEcommerceContext.Cart.API.Models.Entities;
using InfnetEcommerceContext.Cart.API.Repository.DataContext;

namespace InfnetEcommerceContext.Cart.API.Repository.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(ProductEntity productEntity);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly CartContext context;

        public ProductRepository(CartContext context)
        {
            this.context = context;
        }
        public async Task AddProductAsync(ProductEntity productEntity)
        {
            await context.Products.AddAsync(productEntity);
        }
    }
}
