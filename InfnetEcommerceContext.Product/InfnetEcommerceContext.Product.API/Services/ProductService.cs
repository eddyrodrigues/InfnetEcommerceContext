using InfnetEcommerceContext.Product.API.Models.DTOs;
using InfnetEcommerceContext.Product.API.Repository;
using System;

namespace InfnetEcommerceContext.Product.API.Services
{
    public interface IProductService
    {
        ProductDto GetById(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDto GetById(Guid id)
        {
            var productEntity = _productRepository.GetById(id);


            return new ProductDto()
            {
                Description = productEntity.Description,
                Id = productEntity.Id,
                Name = productEntity.Name,
            };
        }


    }
}
