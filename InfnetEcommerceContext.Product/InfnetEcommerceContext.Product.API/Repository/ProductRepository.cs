using InfnetEcommerceContext.Product.API.Models;
using InfnetEcommerceContext.Product.API.Repository.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfnetEcommerceContext.Product.API.Repository
{
    public interface IProductRepository
    {
        ProductEntity GetById(Guid id);

        List<ProductEntity> GetAll();
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public List<ProductEntity> GetAll()
        {
            return _context.Products.ToList();
        }

        public ProductEntity GetById(Guid id) => _context.Products.Find(id);
    }

    
}
