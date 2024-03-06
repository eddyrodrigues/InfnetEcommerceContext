using InfnetEcommerceContext.Product.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InfnetEcommerceContext.Product.API.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Route("{id}")]
        [HttpGet]
        public IActionResult Index([FromServices] IProductRepository productRepository, Guid id)
        {
            return Ok(productRepository.GetById(id));
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index([FromServices] IProductRepository productRepository)
        {
            //    public string Name { get; set; }
            //public string Description { get; set; }
            //public int ProductId { get; set; }
            //public string Image { get; set; }

            return Ok(productRepository.GetAll());
        }

    }
}
