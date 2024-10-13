using InfnetEcommerceContext.Cart.API.Models.DTOs;
using InfnetEcommerceContext.Cart.API.Models.Entities;
using InfnetEcommerceContext.Cart.API.Repository.Repositories;
using InfnetEcommerceContext.Cart.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfnetEcommerceContext.Cart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        private readonly CartService cartService;
        private readonly CheckoutService checkoutService;

        public CartController(ICartRepository cartRepository, CartService cartService, CheckoutService checkoutService)
        {
            this.cartRepository = cartRepository;
            this.cartService = cartService;
            this.checkoutService = checkoutService;
        }
        // GET: api/<CartController>
        [HttpGet]
        public List<CartEntity> Get()
        {
            return cartRepository.GetAll();
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public async Task<CartEntityResponse> GetAsync(Guid id)
        {
            return await cartService.GetById(id);
        }

        // POST api/<CartController>
        [HttpPost]
        public IActionResult Post([FromBody] NewCartRequest value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            if (value.ProductsId == null ||  value.ProductsId.Count == 0)
            {
                return BadRequest(ModelState);
            }

            var cartEntity = new CartEntity();
            cartEntity.UserId = value.UserId;

            cartRepository.Add(cartEntity);
            return Ok(cartEntity);
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

        // GET api/<CartController>/5
        [HttpGet("user-id/{id}")]
        public async Task<CartEntityResponse> GetByUserIdAsync(Guid id)
        {
            return await cartService.GetCartByUserId(id);
        }

        [HttpDelete("user-id/{id}/product/{productId}")]
        public async Task<CartEntityResponse> DeleteCartProductAsync(Guid id, Guid productId)
        {
            return await cartService.DeleteCartProduct(id, productId);
        }

        [HttpPut("user-id/{id}/product/{productId}")]
        public async Task<CartEntityResponse> AddCartProductAsync(Guid id, Guid productId)
        {
            return await cartService.AddCartProduct(id, productId);
        }

        [HttpPost("confirm-checkout")]
        public IActionResult ConfirmCheckout(Guid userId)
        {
            checkoutService.ConfirmCheckout(userId);

            return Ok();
        }
    }
}
