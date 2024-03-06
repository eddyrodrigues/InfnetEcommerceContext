using InfnetEcommerceContext.Order.API.Models.DTOs;
using InfnetEcommerceContext.Order.API.Models.Entities;
using RabbitMQ.Client;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace InfnetEcommerceContext.Order.API.Repository.Repositories
{
    public static class RabbitMQPublish
    {
        public static void PublishUserEmail(UserInfoResponse userInfo, OrderEntity newOrder)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Estamos felizes que comprou conosco, " + userInfo.Name);
            sb.AppendLine("Aqui está seu pedido: ");

            sb.AppendLine("CartId: " + newOrder.CartId);
            sb.AppendLine("UserId: " + newOrder.UserId);
            sb.AppendLine("PaymentId: " + newOrder.PaymentId);
            sb.AppendLine("O valor total da sua compra foi de: " + newOrder.OrderTotal.ToString("C2", new CultureInfo("pt-br")));

            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "NotifyUserEmailListener.queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var emailNotification = new EmailNotification();
            emailNotification.EmailTo = userInfo.Email;
            emailNotification.Subject = "Sua ordem foi criada com sucesso!";
            emailNotification.Body = sb.ToString();

            var message = JsonSerializer.Serialize(emailNotification);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            
            channel.BasicPublish(exchange: string.Empty,
                     "NotifyUserEmailListener.queue",
                     basicProperties: properties,
                     body: body);
        }
    }
}


