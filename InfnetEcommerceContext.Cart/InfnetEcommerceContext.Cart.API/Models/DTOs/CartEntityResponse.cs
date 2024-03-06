using InfnetEcommerceContext.Cart.API.Models.Entities;

namespace InfnetEcommerceContext.Cart.API.Models.DTOs
{
    public class CartEntityResponse
    {
        public CartEntityResponse()
        {
            
        }
        public CartEntityResponse(CartEntity entity)
        {
            UserId = entity.UserId;
            Id = entity.Id;
        }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
    }
}
