namespace MessagingContracts
{
    public class PaymentCreated
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
