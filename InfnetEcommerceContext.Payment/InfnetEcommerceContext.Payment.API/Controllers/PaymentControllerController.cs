using InfnetEcommerceContext.Notification.API.services;
using InfnetEcommerceContext.Payment.API.Models;
using InfnetEcommerceContext.Payment.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InfnetEcommerceContext.Notification.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentControllerController : ControllerBase
    {

        private readonly ILogger<PaymentControllerController> _logger;
        private readonly PaymentService _paymentService;

        public PaymentControllerController(ILogger<PaymentControllerController> logger, PaymentService paymentService)
        {
            _logger = logger;
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
