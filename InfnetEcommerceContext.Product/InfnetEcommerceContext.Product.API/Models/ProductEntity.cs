using System;

namespace InfnetEcommerceContext.Product.API.Models
{
    public class ProductEntity
    {
        protected ProductEntity() {}

        public ProductEntity(string name, string description, decimal price, Guid id)
        {
            Name = name;
            Description = description;
            Price = price;
            Id = id;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageBase64 { get; set; }

    }
}
