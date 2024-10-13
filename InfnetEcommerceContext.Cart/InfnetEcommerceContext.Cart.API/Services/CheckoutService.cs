using System.Text.Json;

namespace InfnetEcommerceContext.Cart.API.Services
{
    public class CheckoutService
    {
        public CheckoutService()
        {
            
        }

        public void ConfirmCheckout(Guid userId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5026");

            var reqCreatePayment = new
            {
                UserId = userId,
            };

            var reqBody = new StringContent(JsonSerializer.Serialize(reqCreatePayment), System.Text.Encoding.UTF8, "application/json");
            
            client.PostAsync("/Payment", reqBody);
        }

    }
}
