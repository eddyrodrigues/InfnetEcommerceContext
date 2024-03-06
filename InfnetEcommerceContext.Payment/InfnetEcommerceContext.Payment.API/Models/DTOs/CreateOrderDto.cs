namespace InfnetEcommerceContext.Payment.API.Models.DTOs
{
    public class CreateOrderDto
    {
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
    }
}
