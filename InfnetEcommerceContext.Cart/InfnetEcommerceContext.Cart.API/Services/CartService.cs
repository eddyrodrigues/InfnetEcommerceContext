using InfnetEcommerceContext.Cart.API.Models.DTOs;
using InfnetEcommerceContext.Cart.API.Models.Entities;
using InfnetEcommerceContext.Cart.API.Repository.Repositories;
using System.Text.Json;

namespace InfnetEcommerceContext.Cart.API.Services
{
    public class CartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<CartEntityResponse> GetById(Guid cartId)
        {
            var cartResponse = new CartEntityResponse();
            var cart = cartRepository.GetById(cartId);
            await FillProducts(cartResponse, cart.Products);
            return cartResponse;
        }
        public async Task<CartEntityResponse> GetCartByUserId(Guid userId)
        {
            var cartResponse = new CartEntityResponse();
            var cart = cartRepository.GetByUserId(userId);
            await FillProducts(cartResponse, cart.Products);
            return cartResponse;
        }
        public async Task<CartEntityResponse> DeleteCartProduct(Guid userId, Guid productId)
        {
            var cart = cartRepository.GetByUserId(userId);
            if (cart == null)
            {
                return null;
            }
            cart.Products = cart.Products.Where(c=> c.ProductId != productId).ToList();
            cartRepository.Update(cart);

            var cartResponse = new CartEntityResponse(cart);
            await FillProducts(cartResponse, cart.Products);
            return cartResponse;
        }

        private async Task FillProducts(CartEntityResponse cartResponse, List<ProductEntity> productResponses)
        {
            foreach (var prod in productResponses)
            {
                var productDto = await GetProductByIdAsync(prod.ProductId);
                if (productDto != null)
                {
                    cartResponse.Products.Add(productDto);
                }
                else
                {
                    cartResponse.Products.Add(new ProductResponse()
                    {
                        Description = "-",
                        Image = "-",
                        Name = "-",
                        Price = 0.0M
                    });
                }
            }
        }

        public async Task<CartEntityResponse> AddCartProduct(Guid userId, Guid productId)
        {
            var cart = cartRepository.GetByUserId(userId);

            var productInformation = await GetProductByIdAsync(productId);
            
            if (productInformation != null)
            {
                var product = new ProductEntity(cart.Id, productInformation.Id);
                cart.Products.Add(product);
                cartRepository.Update(cart);
            }
            var cartResponse = new CartEntityResponse(cart);
            await FillProducts(cartResponse, cart.Products);
            return cartResponse;
        }
        private async Task<ProductResponse> GetProductByIdAsync(Guid productId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://product.api/");
            var response = await client.GetAsync($"/products/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<ProductResponse>(body, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });
            } else
            {
                return null;
            }
            
        }

    }
}
