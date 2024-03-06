using InfnetEcommerceContext.Order.API.Models.DTOs;
using InfnetEcommerceContext.Order.API.Models.Entities;
using InfnetEcommerceContext.Order.API.Repository.Repositories;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System.Text.Json;

namespace InfnetEcommerceContext.Order.API.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly DiscoveryHttpClientHandler _handler;

        public OrderService(IOrderRepository orderRepository, IDiscoveryClient client)
        {
            this._orderRepository = orderRepository;
            _handler = new DiscoveryHttpClientHandler(client);
        }

        public async Task<NewOrderResponse> CreateNewOrder(NewOrderRequest newOrderRequest)
        {

            if (newOrderRequest == null)
            {
                return null;
            }

            var cartInfo = await GetCart(newOrderRequest.CartId);
            var userInfo = await GetUserInformation(cartInfo.UserId);
            var productsTotal = await GetProductsTotal(cartInfo);
            var newOrder = new OrderEntity();
            newOrder.CartId = newOrderRequest.CartId;
            newOrder.UserId = cartInfo.UserId;
            newOrder.OrderTotal = productsTotal;

            var payment = await CreateNewPayment(newOrderRequest);

            if (payment != null)
            {
                newOrder.PaymentId = payment.id;
            } else
            {
                // Delete Order
            }

            _orderRepository.Add(newOrder);


            RabbitMQPublish.PublishUserEmail(userInfo, newOrder);

            return new NewOrderResponse()
            {
                CartId = newOrderRequest.CartId,
                UserId = cartInfo.UserId,
                OrderAmount = productsTotal,
                OrderFee = 0,
                OrderStatus = "Pending",
                PaymentId = payment.id
            };
        }

        private async Task<PaymentEntityResponse> CreateNewPayment(NewOrderRequest newOrderRequest)
        {
            HttpClient client = new HttpClient(_handler, false);
            client.BaseAddress = new Uri("http://payment.api/");
            var response = await client.PostAsJsonAsync($"/paymentcontroller/", newOrderRequest);
            var body = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<PaymentEntityResponse>(body, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        private async Task<UserInfoResponse> GetUserInformation(Guid userId)
        {
            HttpClient client = new HttpClient(_handler, false);
            client.BaseAddress = new Uri("http://user.api/");
            var response = await client.GetAsync($"/user/{userId}");
            var body = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<UserInfoResponse>(body, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        private async Task<CartEntityResponse> GetCart(Guid cartId)
        {
            HttpClient client = new HttpClient(_handler, false);
            client.BaseAddress = new Uri("http://cart.api/");
            var response = await client.GetAsync($"/api/cart/{cartId}");
            var body = response.Content.ReadAsStringAsync().Result;
            var cart = JsonSerializer.Deserialize<CartEntityResponse>(body, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });

            return cart;
        }

        private async Task<decimal> GetProductsTotal(CartEntityResponse cart)
        {
            return cart.Products.Sum(c => c.Price);

        }
    }
}
