using InfnetEcommerceContext.Payment.API.Models;
using InfnetEcommerceContext.Payment.API.Models.DTOs;
using InfnetEcommerceContext.Payment.API.Repository;
using MassTransit;
using MessagingContracts;

namespace InfnetEcommerceContext.Notification.API.services
{
    public class PaymentService
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository, IBus bus)
        {
            _paymentRepository = paymentRepository;
            Bus = bus;
        }

        public IBus Bus { get; }

        public PaymentEntity CreateNewPayment(NewPaymentDTO newPaymentDTO)
        {
            var newlyGeneratedPayment = new PaymentEntity();

            newlyGeneratedPayment.Status = PaymentStatus.Pending;
            newlyGeneratedPayment.UserId = newPaymentDTO.UserId;
            newlyGeneratedPayment.OrderId = newPaymentDTO.OrderId;
            newlyGeneratedPayment.Id = Guid.NewGuid();

            _paymentRepository.Add(newlyGeneratedPayment);

            Bus.Publish(new PaymentCreated()
            {
                UserId = newPaymentDTO.UserId,

            });
            return newlyGeneratedPayment;
        }

        public List<PaymentEntity> GetByUserId(Guid userId) { 
            return _paymentRepository.GetByUserId(userId);
        }
    }
}
