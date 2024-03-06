namespace InfnetEcommerceContext.Payment.API.Models
{
    public class PaymentEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime PaidAt { get; set; }
        public PaymentStatus Status { get; set; }
    }
    
    public enum PaymentStatus
    {
        Pending,
        Paid
    }
}
