using InfnetEcommerceContext.Cart.API.Models.Entities;

namespace InfnetEcommerceContext.Cart.API.Models.DTOs
{
    public class CartEntityResponse
    {
        public CartEntityResponse()
        {
            
        }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
        public int ProductQty => Products.Count;

        public static CartEntityResponse FromEntity(CartEntity entity)
        {
            return new CartEntityResponse()
            {
                UserId = entity.UserId,
                Id = entity.Id,
            };
        }
    }
}
