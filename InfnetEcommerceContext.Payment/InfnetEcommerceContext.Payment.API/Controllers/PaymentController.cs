using InfnetEcommerceContext.Notification.API.services;
using InfnetEcommerceContext.Payment.API.Models;
using InfnetEcommerceContext.Payment.API.Models.DTOs;
using MassTransit;
using MessagingContracts;
using Microsoft.AspNetCore.Mvc;

namespace InfnetEcommerceContext.Notification.API.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly IBus bus;
        private readonly PaymentService _paymentService;

        public PaymentController(IBus bus, PaymentService paymentService)
        {
            
            this.bus = bus;
            this._paymentService = paymentService;
        }

        [Route("")]
        [HttpPost]
        public ActionResult<PaymentEntity> Get([FromBody] NewPaymentDTO newPayment)
        {
            return Ok(_paymentService.CreateNewPayment(newPayment));
        }

        [Route("user-id/{userId}")]
        [HttpPost]
        public IEnumerable<PaymentDTO> Get(Guid userId, [FromBody] CreateOrderDto createOrder)
        {
            return _paymentService.GetByUserId(userId).Select(c => new PaymentDTO(c.Id, c.UserId, c.PaidAt, c.Status, c.OrderId));
        }
    }
}
