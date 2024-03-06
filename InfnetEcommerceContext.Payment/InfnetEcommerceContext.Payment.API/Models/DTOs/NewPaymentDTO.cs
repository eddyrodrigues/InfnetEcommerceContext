namespace InfnetEcommerceContext.Payment.API.Models.DTOs
{
    public class NewPaymentDTO
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
