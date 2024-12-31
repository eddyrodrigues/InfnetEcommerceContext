using InfnetEcommerceContext.Cart.API.Models.Entities;

namespace InfnetEcommerceContext.Cart.API.Factory
{
    public class CartFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="ArgumentNullException">When userId is null</exception>
        /// <returns></returns>
        public static CartEntity Create(Guid userId)
        {
            ArgumentNullException.ThrowIfNull(userId, nameof(userId));
            var cart = new CartEntity();
            cart.UserId = userId;
            cart.Products = new List<ProductEntity>();
            cart.Id = Guid.NewGuid();
            return cart;
        }
    }
}
