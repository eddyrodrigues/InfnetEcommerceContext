namespace InfnetEcommerceContext.Payment.API.Models.DTOs
{
    public class PaymentDTO
    {
        public PaymentDTO()
        {
            
        }

        public PaymentDTO(Guid id, Guid userId, DateTime paidAt, PaymentStatus status, Guid orderId)
        {
            Id = id;
            UserId = userId;
            PaidAt = paidAt;
            _status = status;
            OrderId = orderId;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime PaidAt { get; set; }
        private PaymentStatus _status;
        public string Status => _status.ToString();
    }
}
