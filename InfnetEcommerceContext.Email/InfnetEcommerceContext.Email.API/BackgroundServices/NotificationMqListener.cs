using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InfnetEcommerceContext.Notification.API.BackgroundServices
{

    public class NotificationMqListener<T> : BackgroundService
    {
        private readonly string _queueName;
        protected readonly IServiceProvider _services;
        private IModel _channel;

        public NotificationMqListener(string queueName, IServiceProvider services, IConfiguration configuration)
        {
            _queueName = queueName + ".queue";
            var factory = new ConnectionFactory { HostName = "localhost", UserName = configuration.GetValue<string>("rabbitmq.login"), Password = configuration.GetValue<string>("rabbitmq.password") };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(_queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var objectDeserialized = JsonSerializer.Deserialize<T>(message);
                    if (objectDeserialized != null) ProcessQueue(objectDeserialized);
                }
                finally
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            _channel.BasicConsume(_queueName, false, consumer);

            _services = services;
        }

        // Could also be a async method, that can be awaited in ExecuteAsync above
        public virtual void ProcessQueue(T objectDeserialized)
        {
            Console.WriteLine("Executando ProcessQueue..." + objectDeserialized.ToString());
            //using (var scope = this.services.CreateScope())
            //{
            //    var scopedProcessingService =
            //        scope.ServiceProvider
            //            .GetRequiredService<SendEmailService>();

            //    scopedProcessingService.SendEmail(notifyUser.Email, notifyUser.Subject, notifyUser.Body, null);
            //}
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
