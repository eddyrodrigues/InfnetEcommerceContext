namespace InfnetEcommerceContext.Cart.API.Models.DTOs
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }
        public string Image { get; set; }
    }
}
