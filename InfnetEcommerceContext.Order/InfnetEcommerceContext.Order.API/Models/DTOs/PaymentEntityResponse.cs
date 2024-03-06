namespace InfnetEcommerceContext.Order.API.Models.DTOs
{
    public class PaymentEntityResponse
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid orderId { get; set; }
        public DateTime paidAt { get; set; }
        public int status { get; set; }
    }
}
