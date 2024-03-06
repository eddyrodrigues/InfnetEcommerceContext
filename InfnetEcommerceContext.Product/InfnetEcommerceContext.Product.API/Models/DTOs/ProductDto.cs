using System;

namespace InfnetEcommerceContext.Product.API.Models.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
