using InfnetEcommerceContext.Order.API.Models.DTOs;
using InfnetEcommerceContext.Order.API.Models.Entities;
using InfnetEcommerceContext.Order.API.Repository.Repositories;
using InfnetEcommerceContext.Order.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfnetEcommerceContext.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrderController( OrderService orderService)
        {
            this.orderService = orderService;
        }

        //// GET api/<CartController>/5
        //[HttpGet("{id}")]
        //public OrderEntity Get(Guid id)
        //{
        //    return orderService.GetById(id);
        //}

        // POST api/<CartController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewOrderRequest value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(await orderService.CreateNewOrder(value));
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //// GET api/<CartController>/5
        //[HttpGet("user-id/{id}")]
        //public async Task<CartEntityResponse> GetByUserIdAsync(Guid id)
        //{
        //    return await cartService.GetCartByUserId(id);
        //}

        //[HttpDelete("user-id/{id}/product/{productId}")]
        //public CartEntityResponse DeleteCartProduct(Guid id, Guid productId)
        //{
        //    return cartService.DeleteCartProduct(id, productId);
        //}

        //[HttpPut("user-id/{id}/product/{productId}")]
        //public async Task<CartEntityResponse> AddCartProductAsync(Guid id, Guid productId)
        //{
        //    return await cartService.AddCartProduct(id, productId);
        //}
    }
}
