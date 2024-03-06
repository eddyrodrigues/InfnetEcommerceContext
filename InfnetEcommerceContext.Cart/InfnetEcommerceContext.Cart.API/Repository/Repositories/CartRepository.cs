using InfnetEcommerceContext.Cart.API.Models.Entities;
using InfnetEcommerceContext.Cart.API.Repository.DataContext;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace InfnetEcommerceContext.Cart.API.Repository.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartContext context;

        public CartRepository(CartContext context)
        {
            this.context = context;
        }

        public List<CartEntity> GetAll()
        {
            return context.Carts.Include(c => c.Products).ToList();
        }

        public void Add(CartEntity cart)
        {
            context.Carts.Add(cart);
            context.SaveChanges();
        }
        public void Update(CartEntity cart)
        {
            context.Carts.Update(cart);
            context.SaveChanges();
        }
        public void Delete(CartEntity cart)
        {
            context.Carts.Remove(cart);
            context.SaveChanges();
        }

        public CartEntity GetByUserId(Guid userId)
        {
            return context.Carts.Where(c => c.UserId == userId).Include(c => c.Products).FirstOrDefault();
        }

        public CartEntity GetById(Guid cartId)
        {
            return context.Carts.Where(c => c.Id == cartId).Include(c => c.Products).FirstOrDefault();
        }
    }

    public interface ICartRepository
    {
        void Add(CartEntity cart);
        void Delete(CartEntity cart);
        List<CartEntity> GetAll();
        CartEntity GetById(Guid userId);
        CartEntity GetByUserId(Guid userId);
        void Update(CartEntity cart);
    }
}
