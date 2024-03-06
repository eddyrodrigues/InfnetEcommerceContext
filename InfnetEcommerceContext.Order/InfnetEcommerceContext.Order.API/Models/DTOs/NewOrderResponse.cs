namespace InfnetEcommerceContext.Order.API.Models.DTOs
{
    public class NewOrderResponse
    {
        public Guid CartId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid UserId { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal OrderFee { get; set; } = 0;
        public string OrderStatus { get; set; } 
    }
}
