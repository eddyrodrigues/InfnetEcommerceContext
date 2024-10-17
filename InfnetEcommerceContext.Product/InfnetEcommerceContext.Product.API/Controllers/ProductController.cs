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
            return Ok(productRepository.GetAll());
        }

    }
}
