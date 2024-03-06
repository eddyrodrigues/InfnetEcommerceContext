namespace InfnetEcommerceContext.Order.API.Models.Entities
{
    public class OrderEntity : BaseEntity
    {
        public Guid CartId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid UserId { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
