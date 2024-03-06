using InfnetEcommerceContext.Payment.API.Models;
using InfnetEcommerceContext.Payment.API.Models.DTOs;
using InfnetEcommerceContext.Payment.API.Repository;

namespace InfnetEcommerceContext.Notification.API.services
{
    public class PaymentService
    {
        private readonly PaymentRepository paymentRepository;

        public PaymentService(PaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public PaymentEntity CreateNewPayment(NewPaymentDTO newPaymentDTO)
        {
            var newlyGeneratedPayment = new PaymentEntity();

            newlyGeneratedPayment.Status = PaymentStatus.Pending;
            newlyGeneratedPayment.UserId = newPaymentDTO.UserId;
            newlyGeneratedPayment.OrderId = newPaymentDTO.OrderId;
            newlyGeneratedPayment.Id = Guid.NewGuid();

            paymentRepository.Add(newlyGeneratedPayment);

            return newlyGeneratedPayment;
        }

        public List<PaymentEntity> GetByUserId(Guid userId) { 
            return paymentRepository.GetByUserId(userId);
        }
    }
}
