using InfnetEcommerceContext.Cart.API.Factory;
using InfnetEcommerceContext.Cart.API.Models.DTOs;
using InfnetEcommerceContext.Cart.API.Models.Entities;
using InfnetEcommerceContext.Cart.API.Repository.Repositories;
using NuGet.Versioning;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System.Text.Json;

namespace InfnetEcommerceContext.Cart.API.Services
{
    public class CartService
    {
        private readonly HttpContext _httpContext;
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly ILogger<CartService> _logger;
        private readonly DiscoveryHttpClientHandler _handler;

        public CartService(ICartRepository cartRepository, IDiscoveryClient client, ILogger<CartService> logger, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContext = httpContextAccessor?.HttpContext;
            this.cartRepository = cartRepository;
            _logger = logger;
            this.productRepository = productRepository;
            _handler = new DiscoveryHttpClientHandler(client);
            _handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }


        public async Task<CartEntityResponse> GetCreateUserCart(Guid userId)
        {
            var cartResponse = new CartEntityResponse();
            var cart = cartRepository.GetByUserId(userId);

            if (cart == null)
            {
                cart = CartFactory.Create(userId);
                cartRepository.Add(cart);
                return CartEntityResponse.FromEntity(cart);
            }
            cartResponse = CartEntityResponse.FromEntity(cart);

            await FillProducts(cartResponse, cart.Products);
            return cartResponse;
        }

        public async Task<CartEntityResponse> GetById(Guid cartId)
        {
            var cartResponse = new CartEntityResponse();
            var cart = cartRepository.GetById(cartId);
            if (cart == null)
            {
                cart = new CartEntity()
                {
                    Id = cartId,
                    Products = new List<ProductEntity>(),
                    UserId = new Guid("28345528-4467-452c-ab29-58e020a7fbb0")
                };
                cartRepository.Add(cart);
            }
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
            cart.Products = cart.Products.Where(c => c.ProductId != productId).ToList();
            cartRepository.Update(cart);

            var cartResponse = CartEntityResponse.FromEntity(cart);
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
            var cart = await GetCreateUserCart(userId);

            var productDto = await GetProductByIdAsync(productId);

            if (productDto == null)
            {
                throw new NotFoundProduct();
            }

            var cartUpdate = cartRepository.GetByUserId(userId);
            var newDbProduct = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = productId
            };
            await productRepository.AddProductAsync(newDbProduct);
            cartUpdate.Products.Add(newDbProduct);

            await cartRepository.CommitAsync();

            var product = new ProductResponse()
            {
                Description = productDto.Description,
                Id = productDto.Id,
                Image = productDto.Image,
                Name = productDto.Name,
                Price = productDto.Price,
                ProductId = productDto.ProductId
            };

            cart.Products.Add(product);

            return cart;
        }
        private async Task<ProductResponse> GetProductByIdAsync(Guid productId)
        {
            try
            {
                HttpClient client = new(_handler, false)
                {
                    BaseAddress = new Uri("http://product.api")
                };

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _httpContext?.Request.Headers.Authorization.First().ToString().Replace("Bearer", "", StringComparison.InvariantCultureIgnoreCase));
                var response = await client.GetAsync($"/products/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<ProductResponse>(body, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro on get products from  product.api");
                return null;
            }


        }

    }
}
