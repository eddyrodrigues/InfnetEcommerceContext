namespace InfnetEcommerceContext.Order.API.Models.DTOs
{
    public class CartEntityResponse
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
    }
}
