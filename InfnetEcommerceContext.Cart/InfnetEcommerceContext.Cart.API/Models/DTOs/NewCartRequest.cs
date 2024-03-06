namespace InfnetEcommerceContext.Cart.API.Models.DTOs
{
    public class NewCartRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> ProductsId { get; set; }
    }
}
